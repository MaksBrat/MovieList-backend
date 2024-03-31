using MovieList.Domain.RequestModels.Account;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MovieList.Services.Interfaces
{
    public interface IJWTService
    {
        public string GenerateAccessToken(IEnumerable<Claim> authClaims);
        public string GenerateRefreshToken();
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        public Task<IBaseResponse<AuthenticatedResponse>> RefreshToken(TokenRequest tokenModel);
    }
}
