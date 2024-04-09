using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.Chat;
using MovieList.Hubs;

namespace MovieList.Controllers
{
    [ApiController]
    public class ChatController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly MovieListHub _movieListHub;

        public ChatController(IMessageService messageService, MovieListHub movieListHub)
        {
            _messageService = messageService;
            _movieListHub = movieListHub;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(MessageRequest model)
        {
            var response = _messageService.Create(model, UserId);
            await _movieListHub.SendToAllAsync("ReceiveMessage", response);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var response = await _messageService.Get(pageIndex, pageSize);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            _messageService.Delete(id);
            await _movieListHub.SendToAllAsync("MessageDeleted", id);

            return Ok();
        }
    }
}
