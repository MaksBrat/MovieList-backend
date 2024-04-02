using Microsoft.AspNetCore.SignalR;
using MovieList.Hubs;
using MovieList.Services.Interfaces;

namespace MovieList.Services.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToAllAsync(string method, object obj)
        {
            await _hubContext.Clients.All.SendAsync(method, obj);
        }
    }
}
