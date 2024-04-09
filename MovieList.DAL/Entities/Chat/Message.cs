using MovieList.Domain.Entity.Profile;

namespace MovieList.Domain.Chat
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AuthorId { get; set; }
        public UserProfile Author { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
