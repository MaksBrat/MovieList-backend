namespace MovieList.Domain.DTO.News.Comment
{
    public class CommentRequest
    {
        public int? Id { get; set; }
        public string? Text { get; set; }
        public int ContentId { get; set; }
        public string? DateCreated { get; set; }
    }
}
