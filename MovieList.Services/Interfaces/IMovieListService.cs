using MovieList.Domain.DTO.MovieList;

namespace MovieList.Services.Interfaces
{
    public interface IMovieListService
    {
        Task<List<MovielistItemDTO>> Get(int userId);
        Task<bool> IsMovieInUserList(int movieId, int userId);
        void Add(int userId, int MovieId);
        void Delete(int MovieId, int userId);
        void Update(MovielistItemDTO model);
    }
}
