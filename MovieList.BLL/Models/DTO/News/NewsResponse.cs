using MovieList.Domain.DTO.News.Comment;

namespace MovieList.Domain.ResponseModels.MovieNews
{
    public class NewsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string AvatarUrl { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
        public List<CommentResponse> Comments { get; set; }
    }
}
