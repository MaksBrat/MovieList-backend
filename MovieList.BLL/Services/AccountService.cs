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

            await _profileService.Create(user);

            transactionScope.Complete();
        }
                
        public async Task<AuthenticatedResponse> Login(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            await CheckUserLoginAvailabilityAsync(user, model);

            return await _jwtService.BuildAuthenticatedResponse(user);
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
                throw new CustomizedResponseException((int)HttpStatusCode.InternalServerError, ErrorIdConstans.InternalServerError,
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

        public async Task BlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"User with Id: {user.Id} was not found.");
            }

            SetBlockingDuration(user);

            user.IsBlocked = true;
            user.BlockedTimes += 1;
            user.LastTimeBlocked = DateTime.Now;
            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);
        }

        public async Task UnBlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"User with Id: {user.Id} was not found.");
            }

            user.IsBlocked = false;

            await _userManager.UpdateAsync(user);
        }

        private async Task CheckUserLoginAvailabilityAsync(ApplicationUser user, LoginRequest model)
        {
            if (user == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    "User was not found.");
            }

            if (user.IsBlocked)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    $"User is blocked.");                   
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "Please confirm your email.");
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                    "Failed to login user. Please try again.");
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

        private void SetBlockingDuration(ApplicationUser user)
        {
            switch (user.BlockedTimes)
            {
                case 0:
                    user.BlockedUntil = DateTime.Now.AddMinutes(5);
                    break;
                case 1:
                    user.BlockedUntil = DateTime.Now.AddMinutes(30);
                    break;
                case 2:
                    user.BlockedUntil = DateTime.Now.AddHours(1);
                    break;
                case 3:
                    user.BlockedUntil = DateTime.Now.AddHours(6);
                    break;
                case 4:
                    user.BlockedUntil = DateTime.Now.AddHours(12);
                    break;
                case 5:
                    user.BlockedUntil = DateTime.Now.AddDays(1);
                    break;
                case 6:
                    user.BlockedUntil = DateTime.Now.AddDays(3);
                    break;
                case 7:
                    user.BlockedUntil = DateTime.Now.AddDays(7);
                    break;
                default:
                    user.BlockedUntil = null; // Permanent ban
                    break;
            }
        }
    }
}
