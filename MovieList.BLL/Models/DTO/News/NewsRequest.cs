using System.ComponentModel.DataAnnotations;

namespace MovieList.Domain.DTO.News
{
    public class NewsRequest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
