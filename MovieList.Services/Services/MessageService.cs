using MovieList.DAL.Interfaces;
using MovieList.Domain.Chat;
using MovieList.Domain.RequestModels.Chat;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.Chat;
using MovieList.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entity.Profile;
using System.Net;

namespace MovieList.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        public async Task<IBaseResponse<List<MessageResponse>>> Get(int pageIndex, int pageSize)
        {
            var messageList = await _unitOfWork.GetRepository<Message>().GetPagedListAsync(
                orderBy: x => x.OrderByDescending(x => x.DateCreated),
                include: x => x
                    .Include(x => x.Author)
                        .ThenInclude(x => x.Profile)
                            .ThenInclude(x => x.FileModel),
                pageIndex: pageIndex,
                pageSize: pageSize
            );

            var response = _mapper.Map<List<MessageResponse>>(messageList.Items);

            return new BaseResponse<List<MessageResponse>>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<MessageResponse> Create(MessageRequest model, int userId)
        {        
            var userProfile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            var message = _mapper.Map<Message>(model);
            message.AuthorId = userId;

            _unitOfWork.GetRepository<Message>().Insert(message);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<MessageResponse>(message);
            response.Author = userProfile.Name;
            response.AvatarUrl = userProfile.FileModel.Path;

            return new BaseResponse<MessageResponse>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<bool> Delete(int id)
        {        
            _unitOfWork.GetRepository<Message>().Delete(id);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
