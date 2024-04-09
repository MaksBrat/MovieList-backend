using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Entity.MovieNews
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
