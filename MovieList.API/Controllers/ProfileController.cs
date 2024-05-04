using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieList.Controllers.Base;
using MovieList.Domain.DTO.Profile;

namespace MovieList.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _profileService.Get(UserId);

            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public IActionResult Edit(ProfileRequest profile)
        {          
            var response = _profileService.Edit(profile, UserId);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("change-avatar")] 
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            var response = await _profileService.ChangeAvatar(avatar, UserId);

            return Ok(response);
        }
    }
}
