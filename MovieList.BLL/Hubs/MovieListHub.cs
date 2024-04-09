using Microsoft.AspNetCore.SignalR;
using MovieList.Services.Interfaces;

namespace MovieList.Hubs
{
    public class MovieListHub : Hub
    {   
        private readonly IHubService _hubService;

        public MovieListHub(IHubService hubService)
        {
            _hubService = hubService;
        }

        public override async Task OnConnectedAsync()
        {   
            await Clients.Caller.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = _hubService.GetUserIdByConnectionId(Context.ConnectionId);
            _hubService.RemoveUserIdFromList(userId);
            await base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId() => Context.ConnectionId;

        public void AddUserConnectionId(string userId)
        {
            _hubService.AddUserToList(userId, Context.ConnectionId);
        }

        public async Task SendMessageToUser(string method, string userId, object? message = null)
        {
            var connectionId = _hubService.GetConnectionIdByUserId(userId);
            await Clients.Client(connectionId).SendAsync(method, message);
        }

        public async Task SendToAllAsync(string method, object? message = null)
        {
            await Clients.All.SendAsync(method, message);
        }
    }
}
