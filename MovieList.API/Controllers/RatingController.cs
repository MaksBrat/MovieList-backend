using Microsoft.AspNetCore.Mvc;
using MovieList.BLL.Interfaces;
using MovieList.Controllers.Base;

namespace MovieList.API.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("{movieId}")]
        public IActionResult GetUserRatings(int movieId)
        {
            var response = _ratingService.GetUserRatings(movieId);

            return Ok(response);
        }
    }
}
