using MovieList.Domain.RequestModels.Account;
using MovieList.Domain.ResponseModels.Account;
using System.Security.Claims;

namespace MovieList.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateAccessToken(IEnumerable<Claim> authClaims);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        Task<AuthenticatedResponse> RefreshToken(TokenRequest tokenModel);
    }
}
