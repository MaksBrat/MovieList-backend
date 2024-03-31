using MovieList.Domain.RequestModels.EntitiesFilters.Base;

namespace MovieList.Domain.RequestModels.EntitiesFilters
{
    public class MovieFilterRequest : BaseFilterRequest
    {
        public GenreFilterRequest[]? Genres { get; set; }
        public string? MovieType { get; set; }
        public string? MovieStatus { get; set; }
    }
}
