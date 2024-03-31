using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MovieList.Domain.RequestModels.MovieNews;
using MovieList.Common.Extentions;
using Microsoft.AspNetCore.Authorization;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _userId;

        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;

            _userId = _httpContextAccessor.HttpContext.User?.GetUserId() ?? 0;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CommentRequest model)
        {
            var response = _commentService.Create(model, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpPost("{id}")]
        public IActionResult Get(int id)
        {
            var response = _commentService.Get(id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpGet("getAllByNewsId/{newsId}")]
        public async Task<IActionResult> getAll(int newsId)
        {
            var response = await _commentService.GetAll(newsId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [Authorize]
        [HttpPut]
        public IActionResult Edit(CommentRequest model)
        {
            var response = _commentService.Edit(model);
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
            var response = _commentService.Delete(id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }
    }
}
