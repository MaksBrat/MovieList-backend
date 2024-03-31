using MovieList.Common.Extentions;
using MovieList.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MovieList.Domain.RequestModels.Profile;

namespace MovieList.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private int _userId;

        public ProfileController(IProfileService profileService,
            IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _httpContextAccessor = httpContextAccessor;

            _userId = _httpContextAccessor.HttpContext.User?.GetUserId() ?? 0;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _profileService.Get(_userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpPut]
        public IActionResult Edit(ProfileRequest profile)
        {          
            var response = _profileService.Edit(profile, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new { Message = "Edit profile Successfully" });
            }                      
            return new BadRequestObjectResult(new { Message = response.Description });
        }

        [HttpPut("change-avatar")] 
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            var response = await _profileService.ChangeAvatar(avatar, _userId);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(new { Message = "Avar edited successfully" });
            }
            return new BadRequestObjectResult(new { Message = response.Description });                     
        }
    }
}
