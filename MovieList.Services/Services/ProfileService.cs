using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.Account;
using MovieList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Net;
using MovieList.Domain.ResponseModels.Profile;
using MovieList.Domain.Entity;
using MovieList.Domain.Entity.Profile;
using MovieList.Common.Constants;
using MovieList.Services.Exceptions;
using MovieList.Services.Exceptions.Base;
using MovieList.Core.Interfaces;
using MovieList.Domain.DTO.Profile;

namespace MovieList.Services.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<ProfileResponse> Get(int userId)
        {
            var userProfile = await _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefaultAsync(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (userProfile == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                     $"Profile with Id: {userId} was not found.");
            }

            var response = _mapper.Map<ProfileResponse>(userProfile);

            return response;
        }

        public async Task<ProfileResponse> Create(ApplicationUser user)
        {
            var userProfile = await _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == user.Id);

            if (userProfile != null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                     $"Profile with Id: {user.Id} already exists");
            }

            userProfile = new UserProfile
            {
                Name = $"User{user.Id:00000000}",
                RegistratedAt = DateTime.UtcNow,
                UserId = user.Id,
                FileModel = new FileModel { Name = "user-default-image.png", Path = ProfileConstants.DEFAULT_PROFILE_IMAGE_PATH }
            };

            _unitOfWork.GetRepository<UserProfile>().Insert(userProfile);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<ProfileResponse>(userProfile);

            return response;
        }

        public ProfileResponse Edit(ProfileRequest model, int userId)
        {
            var userProfile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (userProfile == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                     $"Profile with Id: {userId} was not found. Can't edit profile.");
            }

            _mapper.Map(model, userProfile);

            _unitOfWork.GetRepository<UserProfile>().Update(userProfile);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<ProfileResponse>(userProfile);

            return response;         
        }

        public async Task<ProfileResponse> ChangeAvatar(IFormFile avatar, int userId)
        {
            if (avatar == null)
            {
                throw new CustomizedResponseException((int)HttpStatusCode.UnprocessableEntity, ErrorIdConstans.UnprocessableEntity,
                     "Please provide avatar.");
            }

            var userProfile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (userProfile == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                     $"Profile with Id: {userId} was not found. Can't change avatar.");
            }

            if (!string.IsNullOrEmpty(userProfile.FileModel?.Path) && userProfile.FileModel?.Path != ProfileConstants.DEFAULT_PROFILE_IMAGE_PATH)
            {
                _fileService.DeleteFile(userProfile.FileModel?.Path);
            }

            string path = await _fileService.SaveFileAsync(avatar, ProfileConstants.IMAGE_FOLDER_PATH);

            userProfile.FileModel.Path = path;

            _unitOfWork.GetRepository<FileModel>().Update(userProfile.FileModel);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<ProfileResponse>(userProfile);

            return response;   
        }
    }
}
