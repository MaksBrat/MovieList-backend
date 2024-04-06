using MovieList.DAL.Interfaces;
using MovieList.Domain.Chat;
using MovieList.Domain.ResponseModels.Chat;
using MovieList.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entity.Profile;
using MovieList.Services.Exceptions;
using MovieList.Domain.DTO.Chat;

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

        public async Task<List<MessageResponse>> Get(int pageIndex, int pageSize)
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

            if (messageList == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                        $"Messages were not found.");
            }

            var response = _mapper.Map<List<MessageResponse>>(messageList.Items);

            return response;
        }

        public MessageResponse Create(MessageRequest model, int userId)
        {        
            var userProfile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                predicate: x => x.UserId == userId,
                include: i => i
                    .Include(x => x.FileModel));

            if (userProfile == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                        $"Profile with Id: {userId} was not found. Can`t create message");
            }

            var message = _mapper.Map<Message>(model);
            message.AuthorId = userId;

            _unitOfWork.GetRepository<Message>().Insert(message);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<MessageResponse>(message);
            response.Author = userProfile.Name;
            response.AvatarUrl = userProfile.FileModel.Path;

            return response;
        }

        public void Delete(int id)
        {
            var message = _unitOfWork.GetRepository<Message>().GetFirstOrDefault(
                predicate: x => x.Id == id);

            if (message == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Message with Id: {id} was not found. Can't delete message.");
            }

            _unitOfWork.GetRepository<Message>().Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
