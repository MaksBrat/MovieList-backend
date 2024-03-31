using MovieList.Domain.ResponseModels.Genre;

namespace MovieList.Domain.ResponseModels.Movie
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public int? Episodes { get; set; }
        public int? EpisodeDuration { get; set; }
        public string MovieType { get; set; }
        public string MovieStatus { get; set; }
        public string ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public ICollection<GenreResponse> Genres { get; set; }
    }
}
