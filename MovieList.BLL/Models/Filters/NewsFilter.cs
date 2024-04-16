using MovieList.Common.EntitiesFilters.Abstract;
using MovieList.Domain.Entity.MovieNews;

namespace MovieList.Common.EntitiesFilters
{
    public class NewsFilter : BaseFilter<News> 
    {
        public override void ApplyFilter()
        {
            ApplySearchQueryFilter(nameof(News.Title));
        
            ApplyOrderByFilter(OrderBy, AscOrDesc);
        }
    }
}
