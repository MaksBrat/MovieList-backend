using MovieList.Common.Extentions;
using MovieList.Domain.RequestModels.Chat;
using MovieList.Hubs;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessageService _messageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _userId;

        public ChatController(IHubContext<ChatHub> hubContext, IMessageService messageService, IHttpContextAccessor httpContextAccessor)
        {
            _hubContext = hubContext;
            _messageService = messageService;
            _httpContextAccessor = httpContextAccessor;

            _userId = _httpContextAccessor.HttpContext.User?.GetUserId() ?? 0;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(MessageRequest model)
        {
            var response = _messageService.Create(model, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", response.Data);
                return Ok();
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            var response = await _messageService.Get(pageIndex, pageSize);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = _messageService.Delete(id);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await _hubContext.Clients.All.SendAsync("MessageDeleted", id);
                return Ok();
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }
    }
}
