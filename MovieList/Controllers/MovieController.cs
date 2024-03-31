using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MovieList.Domain.RequestModels.Movie;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
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
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MovieFilterRequest filterRequest)
        {
            var response = await _movieService.GetAll(filterRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(MovieRequest model)
        {
            var response = _movieService.Create(model);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }     

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditAsync(MovieRequest model)
        {
            var response = await _movieService.EditAsync(model);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _movieService.Delete(id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }
    }
}
