using MovieList.Domain.RequestModels.Genre;
using System.ComponentModel.DataAnnotations;

namespace MovieList.Domain.RequestModels.Movie
{
    public class MovieRequest
    {
        public int Id { get; set; }

        [Range(1, 10, ErrorMessage = "Invalid rating")]
        public float Rating { get; set; }
        public string? Title { get; set; }
        public int Episodes { get; set; }
        public int EpisodeDuration { get; set; }
        public string? MovieType { get; set; }
        public string MovieStatus { get; set; }
        public string? ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public List<GenreRequest> Genres { get; set; }
    }
}
