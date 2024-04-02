using MovieList.Common.EntitiesFilters.Abstract;
using MovieList.Domain.Entity.MovieNews;

namespace MovieList.Common.EntitiesFilters
{
    public class NewsFilter : BaseFilter<News> 
    {
        public override void CreateFilter()
        {
            ApplySearchQueryFilter(nameof(News.Title));
        
            ApplyOrderByFilter(OrderBy, AscOrDesc);
        }
    }
}
