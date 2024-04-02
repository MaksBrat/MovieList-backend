using MovieList.Domain.RequestModels.MovieNews;
using MovieList.Domain.ResponseModels.MovieNews;

namespace MovieList.Services.Interfaces
{
    public interface ICommentService
    {
        CommentResponse Create(CommentRequest model,int userId);
        CommentResponse Get(int id);
        Task<List<CommentResponse>> GetAll(int newdId);
        CommentResponse Edit(CommentRequest model);     
        void Delete(int id);
    }
}
