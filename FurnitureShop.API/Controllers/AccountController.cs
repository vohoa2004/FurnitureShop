using AutoMapper;
using FurnitureShop.API.DTOs.RequestDTOs;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private IAccountService _accountService;
        private IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById (Guid id)
        {
            try
            {
                return Ok(await _accountService.GetAccountById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/Addresses")]
        public async Task<IActionResult> GetAddressesByCustomer ([FromQuery] Guid id)
        {
            try
            {
                return Ok(await _accountService.GetAddressesByCustomer(id));
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount (Guid id, [FromBody] UpdateAccountRequestDTO dto) {
            try
            {
                UpdateAccountModel model = _mapper.Map<UpdateAccountModel>(dto);
                return Ok(await _accountService.UpdateAccount(id, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            } 
        }
    }
}
