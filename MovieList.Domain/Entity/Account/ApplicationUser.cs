using Microsoft.AspNetCore.Identity;
using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Entity.Account
{
    public class ApplicationUser : IdentityUser<int>
    {   
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public UserProfile Profile { get; set; }
    }
}
