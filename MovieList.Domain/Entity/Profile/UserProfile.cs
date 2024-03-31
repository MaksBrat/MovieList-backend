using MovieList.Domain.Entity;
using MovieList.Domain.Entity.Account;
using MovieList.Domain.Entity.MovieList;

namespace MovieList.Domain.Entity.Profile
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public int FileModelId { get; set; }
        public FileModel FileModel { get; set; }
        public DateTime RegistratedAt { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<MovieListItem> MovieList { get; set; }
    }
}
