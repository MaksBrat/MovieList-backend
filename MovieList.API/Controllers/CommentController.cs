using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MovieList.Domain.RequestModels.MovieNews;
using Microsoft.AspNetCore.Authorization;
using MovieList.Controllers.Base;

namespace MovieList.Controllers
{
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CommentRequest model)
        {
            var response = _commentService.Create(model, UserId);

            return Ok(response);
        }

        [HttpPost("{id}")]
        public IActionResult Get(int id)
        {
            var response = _commentService.Get(id);

            return Ok(response);
        }

        [HttpGet("get-all-by-newsId/{newsId}")]
        public async Task<IActionResult> getAll(int newsId)
        {
            var response = await _commentService.GetAll(newsId);

            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Edit(CommentRequest model)
        {
            var response = _commentService.Edit(model);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _commentService.Delete(id);

            return Ok();
        }
    }
}
