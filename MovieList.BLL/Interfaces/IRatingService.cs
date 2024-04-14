using MovieList.BLL.Models.DTO.Rating;

namespace MovieList.BLL.Interfaces
{
    public interface IRatingService
    {
        Task UpdateMoviesRatingAsync();
        RatingDTO GetUserRatings(int movieId);
    }
}
