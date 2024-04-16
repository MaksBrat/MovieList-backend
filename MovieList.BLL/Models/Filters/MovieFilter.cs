using MovieList.BLL.Models.DTO.Filters;
using MovieList.Common.EntitiesFilters.Abstract;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Enums;
using MovieList.Services.Extentions;
using System.Linq.Expressions;

namespace MovieList.Common.EntitiesFilters
{
    public class MovieFilter : BaseFilter<Movie>
    {       
        public GenreFilterRequest[]? Genres { get; set; }
        public string? MovieType { get; set; }
        public string? MovieStatus { get; set; }
        
        public override void ApplyFilter()
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
            var genres = Genres.Select(x => x.Name).ToList();
            if (genres.Count != 0)
            {
                Expression<Func<Movie, bool>> predicateGenres = a => a.MovieGenres.Where(ag => genres.Contains(ag.Genre.Name)).Count() == genres.Count();
                Predicate = Predicate?.And(predicateGenres) ?? predicateGenres;
            }
        }
    }
}
