using MovieList.Domain.Entity.Account;
using MovieList.Domain.Entity.MovieNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.ResponseModels.MovieNews
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string AvatarUrl { get; set; }
        public int NewsId { get; set; }
        public string DateCreated { get; set; }
    }
}
