using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Domain.RequestModels.MovieListItem
{
    public class MovielistItemRequest
    {
        public int Id { get; set; }
        public float UserRating { get; set; }
        public string MovieStatus { get; set; }
        public int WatchedEpisodes { get; set; }
        public MovieResponse Movie { get; set; }
    }
}
