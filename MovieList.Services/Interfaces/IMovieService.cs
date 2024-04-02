using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.RequestModels.Movie;
using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Services.Interfaces
{
    public interface IMovieService
    {
        void Create(MovieRequest model);
        MovieResponse Get(int id);
        Task<List<MovieResponse>> GetAll(MovieFilterRequest filterRequest);
        Task EditAsync(MovieRequest model);
        void Delete(int id);             
    }
}
