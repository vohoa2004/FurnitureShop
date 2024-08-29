using AutoMapper;
using Azure.Core;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.BLL.Utils;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IAccountRepository accountRepository, 
            IAccountRoleRepository accountRoleRepository, 
            IRoleRepository roleRepository, 
            IConfiguration configuration, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _accountRoleRepository = accountRoleRepository;
            _configuration = configuration;
            _mapper = mapper;

        }


        public async Task<AuthModel> LoginWithUsernameAndPassword(string username, string password)
        {
            var user = await _accountRepository.FindByUsernameAndPassword(username, password) 
                        ?? throw new KeyNotFoundException("Incorrect username or password!"); // specify exception

            // generate access token and response
            var accessToken = await GenerateAccessToken(user);
            var refeshToken = GenerateRefreshToken(username);
            return new AuthModel()
            {
                AccessToken = accessToken,
                RefreshToken = refeshToken
            };
        }

        public async Task<AuthModel> Register(RegisterModel registerModel)
        {
            if ( await _accountRepository.FindByEmail(registerModel.Email) != null)
            {
                throw new Exception("Email existed!");
            }

            if (await _accountRepository.FindByUsername(registerModel.UserName) != null)
            {
                throw new Exception("Username existed!");
            }

            var user = _mapper.Map<Account>(registerModel);
            user.CreatedAt = DateTime.UtcNow;
            
            _accountRepository.CreateAccount(user);
            await _accountRepository.SaveAccount();

            // add roles
            Account userDB = await _accountRepository.FindByUsername(user.UserName);

            user.AccountRoles.Add(new AccountRole() { AccountId = userDB.Id, RoleId = 1 });
            await _accountRepository.SaveAccount();

            // generate access token and response
            var accessToken = await GenerateAccessToken(user);
            var refeshToken = GenerateRefreshToken(user.UserName);
            return new AuthModel()
            {
                AccessToken = accessToken,
                RefreshToken = refeshToken
            };

        }

        private async Task<string> GenerateAccessToken(Account user)
        {
            // Lấy ra danh sách AccountRole bằng user id
            IEnumerable<AccountRole> accountRoles = await _accountRoleRepository.GetByAccountId(user.Id);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString()),
                new Claim("status",user.Status.ToString())
            };

            // Thêm các vai trò của người dùng vào danh sách authClaims
            if (accountRoles != null)
            {
                foreach (var accountRole in accountRoles)
                {
                    var role = await _roleRepository.GetById(accountRole.RoleId);
                    if (role != null)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                    }
                }
            }

            var accessToken = JWTUtils.CreateToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }


        private string GenerateRefreshToken(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };
            var refreshToken = JWTUtils.CreateRefreshToken(claims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }
    }
}
