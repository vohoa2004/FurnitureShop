using AutoMapper;
using FurnitureShop.API.DTOs.RequestDTOs;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FurnitureShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerDto)
        {
            try
            {
                var model = _mapper.Map<RegisterModel>(registerDto);
                var result = await _authService.Register(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LoginWithUsernameAndPassword")]
        public async Task<IActionResult> LoginWithUsernameAndPassword([FromBody] LoginRequestDTO loginDto)
        {
            try
            {
                var result = await _authService.LoginWithUsernameAndPassword(loginDto.Username, loginDto.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Profile")]
        public IActionResult GetUserProfile()
        {
            // Lấy ClaimsPrincipal hiện tại
            var claimsPrincipal = User as ClaimsPrincipal;

            // Lấy các thông tin từ claims
            var userId = claimsPrincipal?.FindFirst("userId")?.Value;
            var username = claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;
            var status = claimsPrincipal?.FindFirst("status")?.Value;
            var email = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            var fullName = claimsPrincipal?.FindFirst("fullName")?.Value;

            var expirationClaim = claimsPrincipal?.FindFirst(JwtRegisteredClaimNames.Exp);
            DateTime? expirationTime = null;

            if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var exp))
            {
                expirationTime = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime.AddHours(7);
            }

            // Lấy danh sách các role
            var roles = claimsPrincipal?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            var userInfo = new
            {
                UserId = userId,
                Username = username,
                Fullname = fullName,
                Status = status,
                Email = email,
                Roles = roles,
                Exp = expirationTime
            };

            return Ok(userInfo);
        }
    }
}
