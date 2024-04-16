using MovieList.BLL.Models.DTO.Rating;

namespace MovieList.BLL.Interfaces
{
    public interface IRatingService
    {
        Task UpdateMoviesRatingAsync();
        Dictionary<int, int> GetUserRatings(int movieId);
    }
}
