using MovieList.Common.EntitiesFilters;
using MovieList.DAL.Interfaces;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.ResponseModels.Movie;
using MovieList.Services.Exceptions;
using MovieList.Core.Interfaces;
using MovieList.Domain.Entity.MovieList;

namespace MovieList.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITmdbService _tmdbService;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper, ITmdbService tmdbService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tmdbService = tmdbService;
        }

        public MovieDTO Get(int id)
        {   
            var movie = _unitOfWork.GetRepository<Movie>().GetFirstOrDefault(
                predicate: x => x.Id == id,
                include: i => i
                        .Include(x => x.MovieGenres)
                            .ThenInclude(x => x.Genre));

            if (movie == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Movie with id: {id} was not found.");
            }

            var response = _mapper.Map<MovieDTO>(movie);

            return response;
        }

        public async Task<List<MovieDTO>> GetAll(MovieFilterRequest filterRequest)
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

            if (moviesPagedList == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Movies were not found.");
            }

            var response = _mapper.Map<List<MovieDTO>>(moviesPagedList.Items);

            return response;
        }

        // TODO: return id?
        public async Task Create(MovieDTO model)
        {
            var movie = _mapper.Map<Movie>(model);

            var response = await _tmdbService.GetMediaAsync(movie.MovieType, movie.Title);
            var tmdbMovie = response.Results.FirstOrDefault();

            movie.TmdbId = tmdbMovie?.Id;
            movie.TmdbRating = tmdbMovie?.VoteAverage;
            movie.Description = tmdbMovie?.Overview;

            _unitOfWork.GetRepository<Movie>().Insert(movie);
            _unitOfWork.SaveChanges();
        }

        public async Task EditAsync(MovieDTO model)
        {
            var movie = await _unitOfWork.GetRepository<Movie>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == model.Id,
                include: i => i
                        .Include(x => x.MovieGenres)
                            .ThenInclude(x => x.Genre));

            if (movie == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                   $"Movie with id: {model.Id} was not found.");
            }

            foreach (var movieGenre in movie.MovieGenres)
            {
                movieGenre.Movie = null;
                movieGenre.Genre = null;
            }

            _unitOfWork.GetRepository<MovieGenre>().DeleteRange(movie.MovieGenres.ToList());

            _mapper.Map(model, movie);

            _unitOfWork.GetRepository<Movie>().Update(movie);
            _unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = _unitOfWork.GetRepository<Movie>().GetFirstOrDefault(
               predicate: x => x.Id == id);

            if (movie == null)
            {
                throw new RecordNotFoundException(ErrorIdConstans.RecordNotFound,
                    $"Movie with Id: {id} was not found. Can't delete movie.");
            }

            _unitOfWork.GetRepository<Movie>().Delete(id);
            _unitOfWork.SaveChanges();
        }

        public async Task CalculateMovieRatingAsync()
        {
            var movies = await _unitOfWork.GetRepository<Movie>().GetAllAsync();

            var movieIds = movies.Select(movie => movie.Id).ToList();
            var ratings = await _unitOfWork.GetRepository<MovieListItem>().GetAllAsync(
                selector: x => new { x.MovieId, x.UserRating },
                predicate: x => movieIds.Contains(x.MovieId) && x.UserRating != null);

            var ratingsGroupedByMovie = ratings.GroupBy(r => r.MovieId)
                                               .ToDictionary(g => g.Key, g => g.Select(r => r.UserRating));

            foreach (var movie in movies)
            {
                if (ratingsGroupedByMovie.TryGetValue(movie.Id, out var movieRatings))
                {
                    var sumOfRatings = (float)movieRatings.Sum() / movieRatings.Count();
                    movie.Rating = sumOfRatings;
                }
            }

            _unitOfWork.GetRepository<Movie>().UpdateRange(movies);
            _unitOfWork.SaveChanges();
        }
    }
}
