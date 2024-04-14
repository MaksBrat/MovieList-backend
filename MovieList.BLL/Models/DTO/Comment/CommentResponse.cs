namespace MovieList.Domain.DTO.News.Comment
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string AvatarUrl { get; set; }
        public int ContentId { get; set; }
        public string DateCreated { get; set; }
    }
}
