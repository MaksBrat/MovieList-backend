using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.MovieList;
using MovieList.Services.Interfaces;

namespace MovieList.Controllers
{
    public class MovieListController : BaseController
    {
        private readonly IMovieListService _movieListService;

        public MovieListController(IMovieListService movieListService)
        {
            _movieListService = movieListService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _movieListService.Get(UserId);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("is-movie-in-user-list/{movieId}")]
        public async Task<IActionResult> IsMoveInUserList(int movieId)
        {
            var response = await _movieListService.IsMovieInUserList(movieId, UserId);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(int movieId)
        {
            _movieListService.Add(UserId, movieId);

            return Ok();
        }

        [Authorize]
        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovieFromList(int movieId)
        {
             _movieListService.Delete(movieId, UserId);

            return Ok();
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update(MovielistItemDTO model)
        {
            _movieListService.Update(model);

            return Ok();
        }
    }
}
