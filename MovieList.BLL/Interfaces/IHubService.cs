namespace MovieList.Services.Interfaces
{
    public interface IHubService
    {
        void AddUserToList(string userId, string connectionId);
        void RemoveUserIdFromList(string userId);
        string GetUserIdByConnectionId(string connectionId);
        string GetConnectionIdByUserId(string userId);
    }
}
