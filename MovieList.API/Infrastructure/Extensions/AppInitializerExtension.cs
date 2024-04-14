using Microsoft.AspNetCore.Identity;
using MovieList.Core.Interfaces;
using MovieList.DAL;
using MovieList.Domain.Entity.Account;
using MovieList.Services.Interfaces;

namespace MovieList.API.Infrastructure.Extensions
{
    public static class AppInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var tmdbService = scope.ServiceProvider.GetRequiredService<ITmdbService>();

                await DbInitializer.Initialize(context, accountService, roleManager, tmdbService);
            }
        }
    }
}
