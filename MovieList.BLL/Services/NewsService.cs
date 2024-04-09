using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.MovieNews;
using MovieList.Domain.ResponseModels.MovieNews;
using MovieList.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MovieList.Common.EntitiesFilters;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.Entity.Profile;
using MovieList.Services.Exceptions;
using MovieList.Domain.DTO.News;

namespace MovieList.Services.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public NewsResponse Get(int id)
        {
            var news = _unitOfWork.GetRepository<News>().GetFirstOrDefault(
                predicate: x => x.Id == id,
                include: i => i.Include(x => x.Author.FileModel));

            if (news == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"News with id: {id} was not found.");
            }

            var response = _mapper.Map<NewsResponse>(news);

            return response;
        }

        public async Task<List<NewsResponse>> GetAll(NewsFilterRequest filterRequest)
        {
            var filter = _mapper.Map<NewsFilter>(filterRequest);

            filter.CreateFilter();

            var news = await _unitOfWork.GetRepository<News>().GetAllAsync(
                predicate: filter.Predicate,
                include: x => x
                    .Include(x => x.Author.FileModel),
                orderBy: filter.OrderByQuery,
                take: filter.Take);

            if (news == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                       $"News were not found.");
            }

            var response = _mapper.Map<List<NewsResponse>>(news);

            return response;
        }

        public NewsResponse Create(NewsRequest model, int userId)
        {
            var userProfile = _unitOfWork.GetRepository<UserProfile>().GetFirstOrDefault(
                    predicate: x => x.UserId == userId,
                    include: i => i
                        .Include(x => x.FileModel));

            if (userProfile == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                     $"Profile with Id: {userId} was not found. Can't create news.");
            }

            var news = _mapper.Map<News>(model);
            news.AuthorId = userId;

            _unitOfWork.GetRepository<News>().Insert(news);
            _unitOfWork.SaveChanges();
             
            var response = _mapper.Map<NewsResponse>(news);
            response.Author = userProfile.Name;
            response.AvatarUrl = userProfile.FileModel.Path;

            return response;
        }    

        public NewsResponse Edit(NewsRequest model)
        {
            var news = _unitOfWork.GetRepository<News>().GetFirstOrDefault(
                predicate: x => x.Id == model.Id);

            if (news == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"News with id: {model.Id} was not found.");
            }

            _mapper.Map(model, news);

            _unitOfWork.GetRepository<News>().Update(news);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<NewsResponse>(news);

            return response;
        }    
        
        public void Delete(int id)
        {
            var news = _unitOfWork.GetRepository<News>().GetFirstOrDefault(
               predicate: x => x.Id == id);

            if (news == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"News with Id: {id} was not found. Can't delete news.");
            }

            _unitOfWork.GetRepository<News>().Delete(id);
            _unitOfWork.SaveChanges();
        }
    }
}
