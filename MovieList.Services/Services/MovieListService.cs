using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieList.DAL.Interfaces;
using MovieList.Domain.DTO.MovieList;
using MovieList.Domain.Entity.MovieList;
using MovieList.Domain.Enums;
using MovieList.Services.Exceptions;
using MovieList.Services.Interfaces;

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

        public async Task<List<MovielistItemDTO>> Get(int userId)
        {
            var movieList = await _unitOfWork.GetRepository<MovieListItem>().GetAllAsync(
                predicate: x => x.ProfileId == userId,
                include: i => i
                    .Include(x => x.Movie)
                        .ThenInclude(x => x.MovieGenres)
                            .ThenInclude(x => x.Genre));

            if (movieList == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Movie list for user with id: {userId} was not found.");
            }

            var movieListResponse = _mapper.Map<List<MovielistItemDTO>>(movieList);

            return movieListResponse;
        }

        public async Task<bool> IsMovieInUserList(int movieId, int userId)
        {
            var movieListItem = await _unitOfWork.GetRepository<MovieListItem>().GetFirstOrDefaultAsync(
                predicate: x => x.ProfileId == userId && x.MovieId == movieId);

            return movieListItem != null;
        }

        public void Add(int userId, int MovieId)
        {
            var movieListItem = new MovieListItem
            {
                MovieId = MovieId,
                ProfileId = userId,
                MovieStatus = MovieListItemStatus.WantToWatch
            };

            _unitOfWork.GetRepository<MovieListItem>().Insert(movieListItem);
            _unitOfWork.SaveChanges();
        }

        public void Update(MovielistItemDTO model)
        {
            var movieListItem = _unitOfWork.GetRepository<MovieListItem>().GetFirstOrDefault(
                predicate: x => x.Id == model.Id);

            if (movieListItem == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Movie list item with id: {model.Id} and movieId: {model.Movie.Id} was not found. Can't update movie list item");
            }

            _mapper.Map(model, movieListItem);

            _unitOfWork.GetRepository<MovieListItem>().Update(movieListItem);
            _unitOfWork.SaveChanges();
        }

        public void Delete(int movieId, int userId)
        {
            var movieListItem = _unitOfWork.GetRepository<MovieListItem>().GetFirstOrDefault(
                predicate: x => x.Movie.Id == movieId && x.ProfileId == userId);

            if (movieListItem == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Movie list item with userId: {userId} and movieId: {movieId} was not found. Can't delete movie from list.");
            }

            _unitOfWork.GetRepository<MovieListItem>().Delete(movieListItem.Id);
            _unitOfWork.SaveChanges();
        }        
    }
}
