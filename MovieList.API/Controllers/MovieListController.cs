using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.MovieList;
using MovieList.Services.Interfaces;

namespace MovieList.Controllers
{
    [ApiController]
    public class MovieListController : BaseController
    {
        private readonly IMovieListService _movieListService;

        public MovieListController(IMovieListService movieListService)
        {
            _movieListService = movieListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _movieListService.Get(UserId);

            return Ok(response);
        }

        [HttpGet("is-movie-in-user-list/{movieId}")]
        public async Task<IActionResult> IsMoveInUserList(int movieId)
        {
            var response = await _movieListService.IsMovieInUserList(movieId, UserId);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Add(int movieId)
        {
            _movieListService.Add(UserId, movieId);

            return Ok();
        }

        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovieFromList(int movieId)
        {
             _movieListService.Delete(movieId, UserId);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(MovielistItemDTO model)
        {
            _movieListService.Update(model);

            return Ok();
        }
    }
}
