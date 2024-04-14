using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Entity.MovieNews
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public int ContentId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
