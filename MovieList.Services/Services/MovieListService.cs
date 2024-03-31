using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.MovieList;
using MovieList.Domain.Enums;
using MovieList.Domain.RequestModels.MovieListItem;
using MovieList.Domain.Response;
using MovieList.Domain.ResponseModels.MovieList;
using MovieList.Services.Interfaces;
using System.Net;

namespace MovieList.Services.Services
{
    public class MovieListService : IMovieListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<List<MovieListItemResponse>>> Get(int userId)
        {
            var movieList = await _unitOfWork.GetRepository<MovieListItem>().GetAllAsync(
                predicate: x => x.ProfileId == userId,
                include: i => i
                    .Include(x => x.Movie)
                        .ThenInclude(x => x.MovieGenres)
                            .ThenInclude(x => x.Genre));

            var movieListResponse = _mapper.Map<List<MovieListItemResponse>>(movieList);

            return new BaseResponse<List<MovieListItemResponse>>()
            {
                Data = movieListResponse,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<IBaseResponse<bool>> IsMovieInUserList(int movieId, int userId)
        {
            var movieListItem = await _unitOfWork.GetRepository<MovieListItem>().GetFirstOrDefaultAsync(
                predicate: x => x.ProfileId == userId && x.MovieId == movieId);

            if(movieListItem == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<bool> Add(int userId, int MovieId)
        {
            var userMovie = new MovieListItem
            {
                MovieId = MovieId,
                ProfileId = userId,
                MovieStatus = MovieListItemStatus.WantToWatch
            };

            _unitOfWork.GetRepository<MovieListItem>().Insert(userMovie);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<bool> Delete(int movieId, int userId)
        {
            var MovieInList = _unitOfWork.GetRepository<MovieListItem>().GetFirstOrDefault(
                predicate: x => x.Movie.Id == movieId && x.ProfileId == userId);

            if (MovieInList == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = "Movie in list is not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            _unitOfWork.GetRepository<MovieListItem>().Delete(MovieInList.Id);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public IBaseResponse<bool> Update(MovielistItemRequest model)
        {
            var movie = _unitOfWork.GetRepository<MovieListItem>().GetFirstOrDefault(
                predicate: x => x.Id == model.Id);

            if (movie == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = "Movie in list is not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            _mapper.Map(model, movie);

            _unitOfWork.GetRepository<MovieListItem>().Update(movie);
            _unitOfWork.SaveChanges();

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
