namespace MovieList.Services.Interfaces
{
    public interface IChatHubService
    {
        Task SendToAllAsync(string method, object obj);
    }
}
