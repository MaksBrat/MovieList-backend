using MovieList.DAL.Interfaces;
using MovieList.DAL.Repository;
using MovieList.DAL.UnitOfWork;
using MovieList.Resources.Logger;
using MovieList.Services.Interfaces;
using MovieList.Services.Services;

namespace MovieList
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieListService, MovieListService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
