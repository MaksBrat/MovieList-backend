using MovieList.Domain.RequestModels.MovieListItem;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.MovieList;

namespace MovieList.Services.Interfaces
{
    public interface IMovieListService
    {
        public Task<IBaseResponse<List<MovieListItemResponse>>> Get(int userId);
        public Task<IBaseResponse<bool>> IsMovieInUserList(int movieId, int userId);
        public IBaseResponse<bool> Add(int userId, int MovieId);
        public IBaseResponse<bool> Delete(int MovieId, int userId);
        public IBaseResponse<bool> Update(MovielistItemRequest model);
    }
}
