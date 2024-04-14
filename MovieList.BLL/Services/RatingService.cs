using AutoMapper;
using MovieList.BLL.Interfaces;
using MovieList.BLL.Models.DTO.Rating;
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
            var ratings = await _unitOfWork.GetRepository<MovieListItem>().GetAllAsync(
                selector: x => new { x.MovieId, x.UserRating },
                predicate: x => movieIds.Contains(x.MovieId) && x.UserRating != null);

            var ratingsGroupedByMovie = ratings.GroupBy(r => r.MovieId)
                                               .ToDictionary(g => g.Key, g => g.Select(r => r.UserRating));

            foreach (var movie in movies)
            {
                if (ratingsGroupedByMovie.TryGetValue(movie.Id, out var movieRatings))
                {
                    var sumOfRatings = (float)movieRatings.Sum() / movieRatings.Count();
                    movie.Rating = sumOfRatings;
                }
            }

            _unitOfWork.GetRepository<Movie>().UpdateRange(movies);
            _unitOfWork.SaveChanges();
        }

        public RatingDTO GetUserRatings(int movieId)
        {
            var ratings = _unitOfWork.GetRepository<MovieListItem>().GetGrouped(
                x => x.UserRating, g => new { Rating = g.Key, Count = g.Count() },
                predicate: x => x.MovieId == movieId);

            var ratingDTO = new RatingDTO
            {
                RatingCounts = new Dictionary<int, int>()
            };

            foreach (var ratingGroup in ratings)
            {
                if (ratingGroup.Rating == null)
                {
                    continue;
                }

                ratingDTO.RatingCounts.Add((int)ratingGroup.Rating, ratingGroup.Count);
            }

            // Ensure all possible ratings are included with counts initialized to zero
            for (int i = 1; i <= 10; i++)
            {
                if (!ratingDTO.RatingCounts.ContainsKey(i))
                {
                    ratingDTO.RatingCounts.Add(i, 0);
                }
            }

            return ratingDTO;
        }
    }
}
