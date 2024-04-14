using MovieList.Domain.DTO.News.Comment;

namespace MovieList.Services.Interfaces
{
    public interface ICommentService
    {
        CommentResponse Create(CommentRequest model,int userId);
        CommentResponse Get(int id);
        Task<List<CommentResponse>> GetAll(int contentId);
        CommentResponse Edit(CommentRequest model);     
        void Delete(int id);
    }
}
