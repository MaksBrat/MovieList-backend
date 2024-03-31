using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.Account;
using MovieList.Domain.Response;
using MovieList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net;
using MovieList.Domain.ResponseModels.Profile;
using MovieList.Common.Extentions;
using MovieList.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using MovieList.Domain.Entity.Profile;
using MovieList.Domain.RequestModels.Profile;
using Microsoft.AspNetCore.Hosting;

namespace MovieList.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IWebHostEnvironment appEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _appEnvironment = appEnvironment;
        }

        public async Task<IBaseResponse<ProfileResponse>> Get(int UserId)
        {
            var profile = await _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefaultAsync(
                predicate: x => x.UserId == UserId,
                include: i => i
                    .Include(x => x.FileModel));

            var response = _mapper.Map<ProfileResponse>(profile);

            return new BaseResponse<ProfileResponse>()
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IBaseResponse<ProfileResponse>> Create(ApplicationUser user)
        {
            var profile = await _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefaultAsync(
            predicate: x => x.Id == user.Id);

            if (profile != null)
            {
                return new BaseResponse<ProfileResponse>
                {
                    Description = "Profile already exists",
                    StatusCode = HttpStatusCode.OK
                };
            }

            var file = new FileModel { Name = "user-default-image.png", Path = _configuration["BlobStorage:UserDefaultImageUrl"] };

            _unitOfWork.GetRepository<FileModel>().Insert(file);
            _unitOfWork.SaveChanges();

            profile = new UserProfile
            {
                Name = $"User{UserIdExtensions.GetId:00000000}",
                RegistratedAt = DateTime.UtcNow,
                UserId = user.Id,
                FileModelId = file.Id
            };

            _unitOfWork.GetRepository<UserProfile>().Insert(profile);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<ProfileResponse>(profile);

            return new BaseResponse<ProfileResponse>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<ProfileResponse> Edit(ProfileRequest model, int userId)
        {
            var profile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (profile == null)
            {
                return new BaseResponse<ProfileResponse>
                {
                    Description = "Profile not found",
                    StatusCode = HttpStatusCode.Found,
                };
            }

            _mapper.Map(model, profile);

            _unitOfWork.GetRepository<UserProfile>().Update(profile);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<ProfileResponse>(profile);

            return new BaseResponse<ProfileResponse>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK,
            };         
        }

        public async Task<IBaseResponse<ProfileResponse>> ChangeAvatar(IFormFile avatar, int userId)
        {
            if (avatar == null)
            {
                return new BaseResponse<ProfileResponse>
                {
                    Description = "Please upload your avatar",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var profile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (profile == null)
            {
                return new BaseResponse<ProfileResponse>
                {
                    Description = "Profile not found",
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            if (!string.IsNullOrEmpty(profile.FileModel?.Path))
            {
                var previousAvatarPath = Path.Combine(_appEnvironment.WebRootPath, profile.FileModel.Path.TrimStart('/'));

                if (File.Exists(previousAvatarPath))
                {
                    File.Delete(previousAvatarPath);
                }
            }

            string path = "/Images/" + avatar.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            profile.FileModel.Path = path;

            _unitOfWork.GetRepository<FileModel>().Update(profile.FileModel);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<ProfileResponse>(profile);

            return new BaseResponse<ProfileResponse>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK,
            };   
        }
    }
}
