using MovieList.Domain.Constants;
using MovieList.Domain.DTO.Tmdb;
using MovieList.Domain.Enums;

namespace MovieList.Core.Interfaces
{
    public interface ITmdbService
    {
        Task<TmdbMovieResponse> GetMediaAsync(MovieType type, string query);
    }
}
