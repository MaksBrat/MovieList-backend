using MovieList.Domain.Entity.Account;
using MovieList.Domain.RequestModels.Account;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.Account;
using Microsoft.AspNetCore.Identity;

namespace MovieList.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IBaseResponse<IdentityUser>> Register(RegisterRequest model);
        Task<IBaseResponse<AuthenticatedResponse>> Login(LoginRequest model);
        public Task<IBaseResponse<ApplicationUser>> Logout(string userName);
    }
}
