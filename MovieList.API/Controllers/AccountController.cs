using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.Account;
using MovieList.Services.Interfaces;

namespace MovieList.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService acccoutService)
        {
            _accountService = acccoutService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            await _accountService.Register(model);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var response = await _accountService.Login(model);

            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        [Route("revoke")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout(User.Identity.Name);

            return Ok();
        }

        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            await _accountService.CreateRoleAsync(name);

            return Ok();
        }

        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            await _accountService.ConfirmEmail(token, email);

            return Ok();
        }
    }
}
