using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Entity.MovieNews
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
