using MovieList.Domain.Entity.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieList.Domain.Entity.Genres
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieGenre> MovieGenre { get; set; }
    }
}
