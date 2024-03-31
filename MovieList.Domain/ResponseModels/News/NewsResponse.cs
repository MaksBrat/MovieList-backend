using MovieList.Domain.Entity.Account;
using MovieList.Domain.Entity.MovieNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ICollection<CommentResponse> Comments { get; set; }
    }
}
