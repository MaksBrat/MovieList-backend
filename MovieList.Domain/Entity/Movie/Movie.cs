using MovieList.Domain.Enums;

namespace MovieList.Domain.Entity.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public int? TmdbId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public float Rating { get; set; }
        public float? TmdbRating { get; set; } 
        public int Episodes { get; set; }
        public int EpisodeDuration { get; set; }
        public MovieType MovieType { get; set; }
        public MovieStatus MovieStatus { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? PosterUrl { get; set; } 
        public string? TrailerUrl { get; set; } 
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }  
}
