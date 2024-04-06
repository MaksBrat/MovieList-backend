using MovieList.Core.Interfaces;
using MovieList.DAL;

namespace MovieList.API.Infrastructure.Extensions
{
    public static class AppInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var tmdbService = scope.ServiceProvider.GetRequiredService<ITmdbService>();
                await DbInitializer.Initialize(context, tmdbService);
            }
        }
    }
}
