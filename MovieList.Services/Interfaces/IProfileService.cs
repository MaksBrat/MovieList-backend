using MovieList.Domain.Entity.Account;
using MovieList.Domain.ResponseModels.Profile;
using Microsoft.AspNetCore.Http;
using MovieList.Domain.RequestModels.Profile;

namespace MovieList.Services.Interfaces
{
    public interface IProfileService
    {
        ProfileResponse Edit(ProfileRequest model, int userId);
        Task<ProfileResponse> ChangeAvatar(IFormFile avatar, int userId);
        Task<ProfileResponse> Create(ApplicationUser user);
        Task<ProfileResponse> Get(int UserId);
    }
}
