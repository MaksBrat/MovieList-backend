using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.Account;
using MovieList.Hubs;
using MovieList.Services.Interfaces;

namespace MovieList.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly MovieListHub _movieListHub;

        public AccountController(IAccountService acccoutService, MovieListHub movieListHub)
        {
            _accountService = acccoutService;
            _movieListHub = movieListHub;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            await _accountService.Register(model);

            return Ok();
        }

        [HttpPost("login")]
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
        [HttpPost("revoke")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout(User.Identity.Name);

            return Ok();
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            await _accountService.CreateRoleAsync(name);

            return Ok();
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            await _accountService.ConfirmEmail(token, email);

            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPost("block-user")]
        public async Task<IActionResult> BlockUser([FromQuery] string userId)
        {
            await _accountService.BlockUser(userId);
            await _movieListHub.SendMessageToUser("UserBlocked", userId);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("unblock-user")]
        public async Task<IActionResult> UnBlockUser(string userId)
        {
            await _accountService.UnBlockUser(userId);

            return Ok();
        }
    }
}
