using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Core.Interfaces;
using MovieList.Domain.Enums;

namespace MovieList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TmdbController : BaseController
    {
        private readonly ITmdbService _tmdbService;

        public TmdbController(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieAsync(MovieType type, string query)
        {
            var result = await _tmdbService.GetMediaAsync(type, query);

            return Ok(result);
        }
    }
}
