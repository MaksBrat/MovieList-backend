using MovieList.Common.EntitiesFilters.Abstract;
using MovieList.Domain.Entity.Movies;

namespace MovieList.Common.EntitiesFilters
{
    public class CommentFilter : BaseFilter<Movie>
    {
        public override void CreateFilter()
        {
            ApplyOrderByFilter(OrderBy, AscOrDesc);
        }
    }
}
