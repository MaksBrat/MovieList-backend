using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MovieList.Domain.RequestModels.MovieNews;
using MovieList.Common.Extentions;
using Microsoft.AspNetCore.Authorization;
using MovieList.Domain.RequestModels.EntitiesFilters;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _userId;

        public NewsController(INewsService newsService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _httpContextAccessor = httpContextAccessor;

            _userId = _httpContextAccessor.HttpContext.User?.GetUserId() ?? 0;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _newsService.Get(id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        // TODO: Paged list
        [HttpGet] 
        public async Task<IActionResult> GetAllNewsWithComments([FromQuery] NewsFilterRequest filterRequest)
        {
            var response = await _newsService.GetAll(filterRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(NewsRequest model)
        {
            var response = _newsService.Create(model, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _newsService.Delete(id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }
    }
}
