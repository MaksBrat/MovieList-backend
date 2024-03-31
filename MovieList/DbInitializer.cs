using MovieList.DAL;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Entity.Genres;

namespace MovieList
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {       
            context.Database.EnsureCreated();

            if (context.Movies.Any())
            {
                return;
            }

            #region Movie

            var movies = new Movie[]
            {
                new Movie
                {
                    Title = "White Album 2",
                    Rating = 9,
                    Episodes = 12,
                    EpisodeDuration = 24,
                    MovieType = Domain.Enums.MovieType.Serial,
                    ReleaseDate = DateTime.Now,
                    PosterUrl = "https://www.nautiljon.com/images/Movie/00/03/white_album_2_2630.webp"
                },
                new Movie
                {
                    Title = "Attack on titan",
                    Rating = 10,
                    Episodes = 22,
                    EpisodeDuration = 24,
                    MovieType = Domain.Enums.MovieType.Serial,
                    ReleaseDate = DateTime.Now,
                    PosterUrl = "https://desu.shikimori.one/uploads/poster/Movies/16498/main-4c0035b92fad430c6721eb2e3779d384.webp"
                },
                new Movie
                {
                    Title = "Death Note",
                    Rating = 10,
                    Episodes = 48,
                    EpisodeDuration = 24,
                    MovieType = Domain.Enums.MovieType.Serial,
                    ReleaseDate = DateTime.Now,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNjRiNmNjMmMtN2U2Yi00ODgxLTk3OTMtMmI1MTI1NjYyZTEzXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg"
                },
                new Movie
                {
                    Title = "Spy x Family",
                    Rating = 8,
                    Episodes = 12,
                    EpisodeDuration = 23,
                    MovieType = Domain.Enums.MovieType.Serial,
                    ReleaseDate = DateTime.Now,
                    PosterUrl = "https://resizing.flixster.com/oiRj9p9jfMCGBa3WA0zJ8TJ8Bwk=/ems.cHJkLWVtcy1hc3NldHMvdHZzZXJpZXMvMzBiMzU5YzktOGJjOC00ODVhLTk0Y2MtYmUwZjA1Yjk5YTgwLnBuZw=="
                },
                new Movie
                {
                    Title = "Fate/Zero",
                    Rating = 9,
                    Episodes = 64,
                    EpisodeDuration = 24,
                    MovieType = Domain.Enums.MovieType.Serial,
                    ReleaseDate = DateTime.Now,
                    PosterUrl = "https://static.wikia.nocookie.net/typemoon/images/f/fe/Fate_zero_Movie_1st_season.jpg/revision/latest?cb=20211001233341"
                },
                new Movie
                {
                    Title = "Golden Time",
                    Rating = 7,
                    Episodes = 12,
                    EpisodeDuration = 24,
                    MovieType = Domain.Enums.MovieType.Serial,
                    ReleaseDate = DateTime.Now,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZTI0MDA5MWUtMWMyYS00NWM3LWE5ZmYtYTUxZmMxMGE5Y2IwXkEyXkFqcGdeQXVyNTIxNDgzOTg@._V1_FMjpg_UX1000_.jpg"
                }
            };

            foreach (Movie movie in movies)
            {
                context.Movies.Add(movie);
            }

            context.SaveChanges();

            var genres = new Genre[]
            {
                new Genre{ Name = "Romance" },
                new Genre{ Name = "Action" },
                new Genre{ Name = "Drama" },
                new Genre{ Name = "Military" },
                new Genre{ Name = "Magic" },
                new Genre{ Name = "Comedy" },
                new Genre{ Name = "History" },
                new Genre{ Name = "Psychological" }
            };

            foreach (Genre genre in genres)
            {
                context.Genres.Add(genre);
            }

            context.SaveChanges();

            var movieGenres = new MovieGenre[]
            {
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "White Album 2").Id,
                    GenreId = genres.Single(x => x.Name == "Romance").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "White Album 2").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Attack on titan").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Death Note").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Death Note").Id,
                    GenreId = genres.Single(x => x.Name == "Psychological").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Death Note").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Spy x Family").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Spy x Family").Id,
                    GenreId = genres.Single(x => x.Name == "Comedy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Fate/Zero").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Fate/Zero").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Fate/Zero").Id,
                    GenreId = genres.Single(x => x.Name == "Magic").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Golden Time").Id,
                    GenreId = genres.Single(x => x.Name == "Romance").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Golden Time").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                }
            };

            foreach (MovieGenre movieGenre in movieGenres)
            {
                var movieGenreInDataBase = context.MovieGenres.Where(
                    s =>
                        s.Movie.Id == movieGenre.MovieId &&
                        s.Genre.Id == movieGenre.GenreId).SingleOrDefault();

                if (movieGenreInDataBase == null)
                {
                    context.MovieGenres.Add(movieGenre);
                }
            }

            context.SaveChanges();

            #endregion           
        }
    }
}
