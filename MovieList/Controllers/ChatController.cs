using MovieList.Domain.RequestModels.Chat;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;

namespace MovieList.Controllers
{
    [ApiController]
    public class ChatController : BaseController
    {
        private readonly IMessageService _messageService;
        private readonly IChatHubService _chatHubService;

        public ChatController(IMessageService messageService, IChatHubService chatHubService)
        {
            _messageService = messageService;
            _chatHubService = chatHubService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(MessageRequest model)
        {
            var response = _messageService.Create(model, UserId);
            await _chatHubService.SendToAllAsync("ReceiveMessage", response);

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
            await _chatHubService.SendToAllAsync("MessageDeleted", id);

            return Ok();
        }
    }
}
