using MovieList.Domain.Entity.Movies;

namespace MovieList.Domain.Entity.Genres
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MovieGenre> MovieGenre { get; set; }
    }
}
