namespace MovieList.Domain.DTO.Tmdb
{
    public class TmdbMovieResponse
    {
        public int Page { get; set; }
        public List<TmdbMovie> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }
}
