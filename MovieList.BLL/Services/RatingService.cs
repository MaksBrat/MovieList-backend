using MovieList.BLL.Interfaces;
using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.MovieList;
using MovieList.Domain.Entity.Movies;

namespace MovieList.BLL.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateMoviesRatingAsync()
        {
            var movies = await _unitOfWork.GetRepository<Movie>().GetAllAsync();

            var movieIds = movies.Select(movie => movie.Id).ToList();
            var ratings = _unitOfWork.GetRepository<MovieListItem>()
                .GetGrouped(
                   x => x.MovieId,
                   g => new { MovieId = g.Key, UserRatings = g.Select(x => x.UserRating) },
                   predicate: x => movieIds.Contains(x.MovieId) && x.UserRating != null)
                .ToDictionary(g => g.MovieId, g => g.UserRatings);

            foreach (var movie in movies)
            {
                if (ratings.TryGetValue(movie.Id, out var movieRatings))
                {
                    var sumOfRatings = (float)movieRatings.Sum() / movieRatings.Count();
                    movie.Rating = sumOfRatings;
                }
            }

            _unitOfWork.GetRepository<Movie>().UpdateRange(movies);
            _unitOfWork.SaveChanges();
        }

        public Dictionary<int, int> GetUserRatings(int movieId)
        {
            var ratingGroups = _unitOfWork.GetRepository<MovieListItem>().GetGrouped(
                x => x.UserRating, g => new { Rating = g.Key, Count = g.Count() },
                predicate: x => x.MovieId == movieId && x.UserRating != null);

            var ratings = Enumerable.Range(1, 10).ToDictionary(r => r, _ => 0);

            foreach (var ratingGroup in ratingGroups)
            {
                ratings[(int)ratingGroup.Rating] = ratingGroup.Count;
            }

            return ratings;
        }
    }
}
