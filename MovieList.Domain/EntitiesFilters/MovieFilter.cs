using MovieList.Common.EntitiesFilters.Abstract;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Enums;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Services.Extentions;
using System.Linq.Expressions;

namespace MovieList.Common.EntitiesFilters
{
    public class MovieFilter : BaseFilter<Movie>
    {       
        public GenreFilterRequest[]? Genres { get; set; }
        public string? MovieType { get; set; }
        public string? MovieStatus { get; set; }
        
        public override void CreateFilter()
        {
            ApplySearchQueryFilter(nameof(Movie.Title));
            ApplyEnumFilter<MovieType>(MovieType, nameof(Movie.MovieType));
            ApplyEnumFilter<MovieStatus>(MovieStatus, nameof(Movie.MovieStatus));

            if(Genres != null)
            {
                ApplyGenreFilter();
            }
            
            ApplyOrderByFilter(OrderBy, AscOrDesc);         
        }

        private void ApplyGenreFilter()
        {
            var genres = Genres.Where(x => x.Checked == true).Select(x => x.Name).ToList();
            if (genres.Count != 0)
            {
                Expression<Func<Movie, bool>> predicateGenres = a => a.MovieGenres.Where(ag => genres.Contains(ag.Genre.Name)).Count() == genres.Count();
                Predicate = Predicate == null ? predicateGenres : Predicate.And(predicateGenres);
            }
        }
    }
}
