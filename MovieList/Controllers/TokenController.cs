using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieList.Domain.RequestModels.Account;
using MovieList.Controllers.Base;

namespace MovieList.Controllers
{
    [ApiController]
    public class TokenController : BaseController
    {
        private readonly IJWTService _jwtService;

        public TokenController(IJWTService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenRequest model)
        {
            var response = await _jwtService.RefreshToken(model);

            return Ok(response);
        }
    }
}
