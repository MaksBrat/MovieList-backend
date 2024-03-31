using MovieList.Common.EntitiesFilters;
using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.Response;
using MovieList.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using MovieList.Domain.RequestModels.Movie;
using MovieList.Domain.ResponseModels.Movie;

namespace MovieList.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
             
        public IBaseResponse<bool> Create(MovieRequest model)
        {
            var movie = _mapper.Map<Movie>(model);

            _unitOfWork.GetRepository<Movie>().Insert(movie);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };       
        }

        public IBaseResponse<MovieResponse> Get(int id)
        {   
            var movie = _unitOfWork.GetRepository<Movie>().GetFirstOrDefault(
                predicate: x => x.Id == id,
                include: i => i
                        .Include(x => x.MovieGenres)
                            .ThenInclude(x => x.Genre));

            if (movie == null)
            {
                return new BaseResponse<MovieResponse>
                {
                    Description = "Movie not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var response = _mapper.Map<MovieResponse>(movie);

            return new BaseResponse<MovieResponse>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IBaseResponse<List<MovieResponse>>> GetAll(MovieFilterRequest filterRequest)
        {
            var filter = _mapper.Map<MovieFilter>(filterRequest);

            filter.CreateFilter();

            var moviesPagedList = await _unitOfWork.GetRepository<Movie>().GetPagedListAsync(
                predicate: filter.Predicate,
                include: x => x
                    .Include(x => x.MovieGenres)
                        .ThenInclude(x => x.Genre),
                orderBy: filter.OrderByQuery,
                pageSize: filter.Take,
                pageIndex: filter.PageIndex);

            var response = _mapper.Map<List<MovieResponse>>(moviesPagedList.Items);

            return new BaseResponse<List<MovieResponse>>
            {
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IBaseResponse<bool>> EditAsync(MovieRequest model)
        {
            var movie = await _unitOfWork.GetRepository<Movie>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == model.Id,
                include: i => i
                        .Include(x => x.MovieGenres)
                            .ThenInclude(x => x.Genre));

            if (movie == null)
            {
                return new BaseResponse<bool>
                {
                    Description = "Movie not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            foreach (var movieGenre in movie.MovieGenres)
            {
                movieGenre.Movie = null;
                movieGenre.Genre = null;
            }

            _unitOfWork.GetRepository<MovieGenre>().DeleteRange(movie.MovieGenres.ToList());
            _unitOfWork.SaveChanges();

            _mapper.Map(model, movie);

            _unitOfWork.GetRepository<Movie>().Update(movie);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<bool> Delete(int id)
        {
            _unitOfWork.GetRepository<Movie>().Delete(id);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
