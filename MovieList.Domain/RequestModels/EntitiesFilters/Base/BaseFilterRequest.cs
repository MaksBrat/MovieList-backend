namespace MovieList.Domain.RequestModels.EntitiesFilters.Base
{
    public class BaseFilterRequest
    {
        public string? SearchQuery { get; set; }
        public string? OrderBy { get; set; }
        public string? AscOrDesc { get; set; }
        public int Take { get; set; } = 0;
        public int PageIndex { get; set; } = 0;
    }
}
