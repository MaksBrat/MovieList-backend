using MovieList.Services.Interfaces;
using System.Collections.Concurrent;

namespace MovieList.Services.Services
{
    public class HubService : IHubService
    {
        private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public void AddUserToList(string userId, string connectionId)
            => Users.TryAdd(userId, connectionId);

        public void RemoveUserIdFromList(string userId)
            => Users.TryRemove(userId, out _);

        public string GetUserIdByConnectionId(string connectionId)
            => Users.FirstOrDefault(x => x.Value == connectionId).Key;

        public string GetConnectionIdByUserId(string userId)
        {
            Users.TryGetValue(userId, out string connectionId);
            return connectionId;
        }
    }
}
