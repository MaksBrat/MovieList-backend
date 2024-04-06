using MovieList.Domain.Entity.Account;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Net;
using MovieList.Domain.ResponseModels.Account;
using MovieList.Services.Exceptions.Base;
using MovieList.Services.Exceptions;
using MovieList.Domain.DTO.Email;
using MovieList.Domain.Constants;
using MovieList.Domain.DTO.Account;
using MovieList.DAL.Interfaces;

namespace MovieList.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJWTService _jwtService;
        private readonly IProfileService _profileService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJWTService jwtService, 
            IProfileService profileService, IEmailService emailService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _profileService = profileService;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
        }

        public async Task Register(RegisterRequest model)
        {
            using var transactionScope = _unitOfWork.BeginTransaction();

            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "User with this email already exists.");
            }

            var user = new ApplicationUser()
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

            await SendConfirmationEmail(user);

            // TODO: return and store user profile in lc or cashe
            await _profileService.Create(user);

            transactionScope.Complete();
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

            var response = await _jwtService.BuildAuthenticatedResponse(user);

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
            var result = await _roleManager.CreateAsync(new ApplicationRole() { Name = name });

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
                    "Failed to confirm email. Please try again.");
            }
        }

        private async Task SendConfirmationEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = CreateConfirmationLink(token, user.Email);
            var message = new EmailMessage(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            await _emailService.SendEmailAsync(message);
        }

        private string CreateConfirmationLink(string token, string email) 
            => $"{AccountConstants.CONFIRM_EMAIL_ENDPOINT}token={token}&email={email}";
    }
}
