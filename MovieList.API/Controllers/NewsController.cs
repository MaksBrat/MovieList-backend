using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.News;

namespace MovieList.Controllers
{
    public class NewsController : BaseController
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _newsService.Get(id);

            return Ok(response);
        }

        // TODO: Paged list ?
        [HttpGet] 
        public async Task<IActionResult> GetAllNewsWithComments([FromQuery] NewsFilterRequest filterRequest)
        {
            var response = await _newsService.GetAll(filterRequest);

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(NewsRequest model)
        {
            var response = _newsService.Create(model, UserId);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _newsService.Delete(id);

            return Ok();
        }
    }
}
