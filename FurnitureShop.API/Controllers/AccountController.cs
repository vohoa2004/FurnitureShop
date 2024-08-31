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

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}/Addresses")]
        public async Task<IActionResult> GetAddressesByCustomer (Guid id)
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
    }
}
