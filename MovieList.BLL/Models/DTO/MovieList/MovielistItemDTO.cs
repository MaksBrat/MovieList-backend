using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Domain.DTO.MovieList
{
    public class MovielistItemDTO
    {
        public int Id { get; set; }
        public float? UserRating { get; set; }
        public string MovieStatus { get; set; }
        public int WatchedEpisodes { get; set; }
        public MovieDTO Movie { get; set; }
    }
}
