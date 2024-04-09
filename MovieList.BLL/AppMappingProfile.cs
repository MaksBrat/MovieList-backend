using MovieList.Common.Constants;
using MovieList.Common.EntitiesFilters;
using MovieList.Common.Utility;
using MovieList.Domain.Chat;
using MovieList.Domain.Entity.MovieNews;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.RequestModels.EntitiesFilters;
using MovieList.Domain.ResponseModels.MovieNews;
using MovieList.Domain.ResponseModels.Chat;
using MovieList.Domain.ResponseModels.Profile;
using MovieList.Domain.Entity.MovieList;
using MovieList.Domain.Entity.Profile;
using System.Globalization;
using MovieList.Domain.ResponseModels.Movie;
using MovieList.Domain.ResponseModels.Genre;
using MovieList.Domain.DTO.News;
using MovieList.Domain.DTO.Chat;
using MovieList.Domain.DTO.Profile;
using MovieList.Domain.DTO.MovieList;
using MovieList.Domain.DTO.News.Comment;

namespace MovieList.Services
{
    public class AppMappingProfile : AutoMapper.Profile
    {
        public AppMappingProfile()
        {

            #region Filters

            CreateMap<MovieFilterRequest, MovieFilter>();
            CreateMap<NewsFilterRequest, NewsFilter>();
            CreateMap<CommentFilterRequest, CommentFilter>();

            #endregion

            #region Movie

            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres
                    .Select(x => new GenreDTO
                    {
                        Id = x.GenreId,
                        Name = x.Genre.Name
                    }).ToList()))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToString("yyyy-MM-dd")));

            CreateMap<MovieDTO, Movie>()
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.Genres
                .Select(x => new MovieGenre
                {   
                    GenreId = x.Id,
                }).ToList()))
                .ForMember(dest => dest.TrailerUrl, opt => opt.MapFrom(src => src.TrailerUrl != null ? UrlParser.ParseTrailerUrl(src.TrailerUrl) : null))
                .ForMember(dest => dest.PosterUrl, opt => opt.MapFrom(src => src.PosterUrl != null ? src.PosterUrl : MovieConstants.POSTER_URL))
                .ForMember(dest => dest.ReleaseDate, opt =>
                    opt.MapFrom(src => DateTime.ParseExact(src.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                ));

            #endregion

            #region Profile

            CreateMap<ProfileRequest, UserProfile>();

            CreateMap<UserProfile, ProfileResponse>()
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.FileModel.Path));

            #endregion

            #region MovieList

            CreateMap<MovieListItem, MovielistItemDTO>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie));

            CreateMap<MovielistItemDTO, MovieListItem>()
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.Movie.Id))
                .ForMember(dest => dest.Profile, opt => opt.Ignore())
                .ForMember(dest => dest.Movie, opt => opt.Ignore());

            #endregion

            #region News

            CreateMap<News, NewsResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Author.FileModel.Path))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated.ToString("yyyy-MM-dd")));

            CreateMap<NewsRequest, News>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now));

            #endregion

            #region Comment

            CreateMap<Comment, CommentResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Author.FileModel.Path))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated.ToString("yyyy-MM-dd")));

            CreateMap<CommentRequest, Comment>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now));


            #endregion

            #region Message

            CreateMap<Message, MessageResponse>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.Author.FileModel.Path))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated.ToString("yyyy-MM-dd"))); ;

            CreateMap<MessageRequest,  Message>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now));

            #endregion
        }
    }
}
