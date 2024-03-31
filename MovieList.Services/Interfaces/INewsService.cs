using MovieList.Domain.RequestModels.MovieNews;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.MovieNews;

namespace MovieList.Services.Interfaces
{
    public interface INewsService
    {
        public IBaseResponse<NewsResponse> Create(NewsRequest model, int userId);
        public IBaseResponse<NewsResponse> Get(int id);
        public Task<IBaseResponse<List<NewsResponse>>> GetAll(NewsFilterRequest filter);    
        public Task<IBaseResponse<bool>> Delete(int id);
    }
}
