using MovieList.Domain.Entity.Account;
using MovieList.Domain.RequestModels.Account;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using MovieList.Domain.ResponseModels.Account;
using MovieList.Services.Exceptions.Base;
using MovieList.Services.Exceptions;
using MovieList.Domain.DTO.Email;

namespace MovieList.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJWTService _jwtService;
        private readonly IProfileService _profileService;
        private readonly IEmailService _emailService;

        private readonly IConfiguration _configuration;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJWTService jwtService, 
            IProfileService profileService, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _profileService = profileService;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task Register(RegisterRequest model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "User with this email already exists.");
            }

            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            result = await _userManager.AddToRoleAsync(user, "Admin");

            if (!result.Succeeded)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
                    "Failed to create user. Please try again.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = CreateConfirmationLink(token, user.Email);
            var message = new EmailMessage(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            await _emailService.SendEmailAsync(message);

            await _profileService.Create(user);
        }

        public async Task<AuthenticatedResponse> Login(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"User with Id: {user.Id} was not found.");
            }

            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "Please confirm your email.");
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "Failed to login user. Please try again.");
            }

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

            var accessToken = _jwtService.GenerateAccessToken(authClaims);
            var refreshToken = _jwtService.GenerateRefreshToken();

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

        public async Task Logout(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    "User was not found.");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }
        
        public async Task CreateRoleAsync(string name)
        {
            var result =  await _roleManager.CreateAsync(new ApplicationRole() { Name = name });

            if (!result.Succeeded)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.FailedToInsert,
                    $"Can't add role with name: {name}");
            }
        }

        public async Task ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"User was not found. Can't confirm email.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "Failed to confirm email. Invalid token. Please try again.");
            }
        }

        private string CreateConfirmationLink(string token, string email)
        {
            // TODO: make a constant

            return $"http://localhost:4200/confirm-email?token={token}&email={email}";
        }
    }
}
