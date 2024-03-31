using MovieList.Domain.RequestModels.MovieNews;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.MovieNews;

namespace MovieList.Services.Interfaces
{
    public interface ICommentService
    {
        public IBaseResponse<CommentResponse> Create(CommentRequest model,int userId);
        public IBaseResponse<CommentResponse> Get(int id);
        public Task<IBaseResponse<List<CommentResponse>>> GetAll(int newdId);
        public IBaseResponse<CommentResponse> Edit(CommentRequest model);     
        public IBaseResponse<bool> Delete(int id);
    }
}
