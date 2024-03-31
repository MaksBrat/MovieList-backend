using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.RequestModels.Movie;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Services.Interfaces
{
    public interface IMovieService
    {
        IBaseResponse<bool> Create(MovieRequest model);
        IBaseResponse<MovieResponse> Get(int id);
        Task<IBaseResponse<List<MovieResponse>>> GetAll(MovieFilterRequest filterRequest);
        Task<IBaseResponse<bool>> EditAsync(MovieRequest model);
        IBaseResponse<bool> Delete(int id);             
    }
}
