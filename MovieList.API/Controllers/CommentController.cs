using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.News.Comment;

namespace MovieList.Controllers
{
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

        [HttpGet("get-all-by-contentId/{contentId}")]
        public async Task<IActionResult> getAll(int contentId)
        {
            var response = await _commentService.GetAll(contentId);

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
