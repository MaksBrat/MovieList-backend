using MovieList.Domain.RequestModels.MovieNews;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.ResponseModels.MovieNews;

namespace MovieList.Services.Interfaces
{
    public interface INewsService
    {
        NewsResponse Create(NewsRequest model, int userId);
        NewsResponse Get(int id);
        Task<List<NewsResponse>> GetAll(NewsFilterRequest filter);    
        void Delete(int id);
    }
}
