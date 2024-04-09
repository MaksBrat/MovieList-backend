using MovieList.Domain.Entity.Account;
using MovieList.Domain.ResponseModels.Account;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MovieList.Services.Exceptions;
using System.Net;
using MovieList.Services.Exceptions.Base;
using MovieList.Domain.DTO.Account;

namespace MovieList.Services.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JWTService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthenticatedResponse> BuildAuthenticatedResponse(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var accessToken = GenerateAccessToken(authClaims);
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            var response = new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id
            };

            return response;
        }

        public async Task<AuthenticatedResponse> RefreshToken(TokenRequest tokenModel)
        {
            if (tokenModel == null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.BadRequest, ErrorIdConstans.BadRequest,
                    "Invalid client request");
            }

            string accessToken = tokenModel.AccessToken!;
            string refreshToken = tokenModel.RefreshToken!;

            var principal = GetPrincipalFromExpiredToken(accessToken);

            string userName = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                        $"User with name: {userName} was not found.");
            }

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "Invalid refresh token");
            }

            var newAccessToken = GenerateAccessToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            var response = new AuthenticatedResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return response;
        }

        private string GenerateAccessToken(IEnumerable<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            _ = int.TryParse(_configuration["Jwt:TokenValidityInDays"], out int TokenValidityInDays);

            var tokenOptions = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                authClaims,
                expires: DateTime.Now.AddDays(TokenValidityInDays),
                signingCredentials: signIn);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
                
            return principal;
        }
    }
}
