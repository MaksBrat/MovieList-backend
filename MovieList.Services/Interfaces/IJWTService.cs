using MovieList.Domain.DTO.Account;
using MovieList.Domain.Entity.Account;
using MovieList.Domain.ResponseModels.Account;

namespace MovieList.Services.Interfaces
{
    public interface IJWTService
    {
        Task<AuthenticatedResponse> BuildAuthenticatedResponse(ApplicationUser user);
        Task<AuthenticatedResponse> RefreshToken(TokenRequest tokenModel);
    }
}
