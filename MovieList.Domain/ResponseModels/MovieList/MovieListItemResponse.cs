using MovieList.Domain.Entity.Account;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.ResponseModels.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieList.Domain.ResponseModels.MovieList
{
    public class MovieListItemResponse
    {
        public int Id { get; set; }
        public float UserRating { get; set; }
        public string MovieStatus { get; set; }
        public int WatchedEpisodes { get; set; }
        public MovieResponse Movie { get; set; }
    }
}
