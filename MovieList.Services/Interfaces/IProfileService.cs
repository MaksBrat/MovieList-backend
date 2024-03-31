using MovieList.Domain.Entity.Account;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.Profile;
using Microsoft.AspNetCore.Http;
using MovieList.Domain.RequestModels.Profile;
using MovieList.Domain.ResponseModels.MovieList;
using MovieList.Domain.RequestModels.MovieListItem;

namespace MovieList.Services.Interfaces
{
    public interface IProfileService
    {
        public IBaseResponse<ProfileResponse> Edit(ProfileRequest model, int userId);
        public Task<IBaseResponse<ProfileResponse>> ChangeAvatar(IFormFile avatar, int userId);
        public Task<IBaseResponse<ProfileResponse>> Create(ApplicationUser user);
        public Task<IBaseResponse<ProfileResponse>> Get(int UserId);
    }
}
