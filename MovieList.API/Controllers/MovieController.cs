using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Controllers
{
    [ApiController]
    public class MovieController : BaseController
    {   
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _movieService.Get(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MovieFilterRequest filterRequest)
        {
            var response = await _movieService.GetAll(filterRequest);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MovieDTO model)
        {
            await _movieService.Create(model);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditAsync(MovieDTO model)
        {
            await _movieService.EditAsync(model);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _movieService.Delete(id);

            return Ok();
        }

        [HttpPost("rating")]
        public async Task<IActionResult> CalculateRating()
        {
            await _movieService.CalculateMovieRatingAsync();

            return Ok();
        }
    }
}
