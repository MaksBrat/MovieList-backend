using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Services.Interfaces
{
    public interface IMovieService
    {
        Task Create(MovieDTO model);
        MovieDTO Get(int id);
        Task<List<MovieDTO>> GetAll(MovieFilterRequest filterRequest);
        Task EditAsync(MovieDTO model);
        void Delete(int id);
    }
}
