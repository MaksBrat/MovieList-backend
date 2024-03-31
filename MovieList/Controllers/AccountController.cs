using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieList.Domain.Entity.Account;
using MovieList.Domain.RequestModels.Account;
using MovieList.Services.Interfaces;
using System.Net;

namespace MovieList.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private RoleManager<ApplicationRole> _roleManager;

        public AccountController(IAccountService acccoutService, RoleManager<ApplicationRole> roleManager)
        {
            _accountService = acccoutService;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            var response = await _accountService.Register(model);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new { Message = "User created successfully." });
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var response = await _accountService.Login(model);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke")]
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name;
            var response = await _accountService.Logout(userName);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return NoContent();
            }
            return BadRequest(response.Description);
        }

        [HttpPost]
        [Route("createRole")]
        public async Task<IActionResult> CreateRole([FromQuery] string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });
            if (result.Succeeded)
            {
                return Ok(new { Message = "Role Created Successfully" });
            }
            return new BadRequestObjectResult(new { Message = "Failed to create role" });
        }
    }
}
