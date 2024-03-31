﻿using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieList.Domain.RequestModels.Account;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
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
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return BadRequest("Invalid client request");
        }
    }
}
