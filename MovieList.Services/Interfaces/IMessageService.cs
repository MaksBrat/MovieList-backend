using MovieList.Domain.RequestModels.Chat;
using MovieList.Domain.ResponseModels.Chat;

namespace MovieList.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageResponse>> Get(int pageIndex, int pageSize);
        MessageResponse Create(MessageRequest model, int userId);
        void Delete(int id);
    }
}
