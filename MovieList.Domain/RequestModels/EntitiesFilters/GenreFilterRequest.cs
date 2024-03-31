using MovieList.Domain.RequestModels.EntitiesFilters.Base;

namespace MovieList.Domain.RequestModels.EntitiesFilters
{
    public class GenreFilterRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool? Checked { get; set; }
    }
}
