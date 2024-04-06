using MovieList.Domain.DTO.Account;
using MovieList.Domain.ResponseModels.Account;

namespace MovieList.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterRequest model);
        Task<AuthenticatedResponse> Login(LoginRequest model);
        Task Logout(string userName);
        Task CreateRoleAsync(string name);
        Task ConfirmEmail(string token, string email);
    }
}
