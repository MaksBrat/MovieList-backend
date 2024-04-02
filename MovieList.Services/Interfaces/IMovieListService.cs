using MovieList.Domain.RequestModels.MovieListItem;
using MovieList.Domain.ResponseModels.MovieList;

namespace MovieList.Services.Interfaces
{
    public interface IMovieListService
    {
        Task<List<MovieListItemResponse>> Get(int userId);
        Task<bool> IsMovieInUserList(int movieId, int userId);
        void Add(int userId, int MovieId);
        void Delete(int MovieId, int userId);
        void Update(MovielistItemRequest model);
    }
}
