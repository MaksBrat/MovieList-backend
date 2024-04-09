using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Enums;
using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Entity.MovieList
{
    public class MovieListItem
    {
        public int Id { get; set; }
        public int? UserRating { get; set; }
        public int? WatchedEpisodes { get; set; }
        public MovieListItemStatus MovieStatus { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}
