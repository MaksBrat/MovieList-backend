using MovieList.Domain.Entity.Account;
using MovieList.Domain.Entity.MovieNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.RequestModels.MovieNews
{
    public class NewsRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
