using Microsoft.AspNetCore.Mvc;
using MovieList.Common.Extentions;
using MovieList.Domain.RequestModels.MovieListItem;
using MovieList.Services.Interfaces;
using System.Net;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieListController : ControllerBase
    {
        private readonly IMovieListService _movieListService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _userId;

        public MovieListController(IMovieListService movieListService,
            IHttpContextAccessor httpContextAccessor)
        {
            _movieListService = movieListService;
            _httpContextAccessor = httpContextAccessor;

            _userId = _httpContextAccessor.HttpContext.User?.GetUserId() ?? 0;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _movieListService.Get(_userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpGet("isMovieInUserList/{movieId}")]
        public async Task<IActionResult> IsMoveInUserList(int movieId)
        {
            var response = await _movieListService.IsMovieInUserList(movieId, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpPost]
        public IActionResult Add(int movieId)
        {
            var response = _movieListService.Add(_userId, movieId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovieFromList(int movieId)
        {
            var response = _movieListService.Delete(movieId, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpPut]
        public IActionResult ChangeMovieStatus(MovielistItemRequest model)
        {
            var response = _movieListService.Update(model);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }
    }
}
