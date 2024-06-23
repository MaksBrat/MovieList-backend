using MovieList.Domain.ResponseModels.Genre;
using System.ComponentModel.DataAnnotations;

namespace MovieList.Domain.ResponseModels.Movie
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public int? TmdbId { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        public float Rating { get; set; }
        public float? TmdbRating { get; set; }
        public int? Episodes { get; set; }
        public int? EpisodeDuration { get; set; }
        public string MovieType { get; set; }
        public string MovieStatus { get; set; }
        public string ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public List<GenreDTO> Genres { get; set; }
    }
}
