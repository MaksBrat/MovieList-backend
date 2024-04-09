using MovieList.DAL;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Entity.Genres;
using MovieList.Core.Interfaces;

namespace MovieList.API.Infrastructure
{
    public class DbInitializer
    {
        public async static Task Initialize(ApplicationDbContext context, ITmdbService tmdbService)
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
                    Title = "The Shawshank Redemption",
                    Rating = 9.3f,
                    Episodes = 1,
                    EpisodeDuration = 142,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1994, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://humanehollywood.org/app/uploads/2020/06/5KCVkau1HEl7ZzfPsKAPM0sMiKc-scaled.jpg",
                },
                new Movie
                {
                    Title = "The Godfather",
                    Rating = 9.2f,
                    Episodes = 1,
                    EpisodeDuration = 184,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1972, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BM2MyNjYxNmUtYTAwNi00MTYxLWJmNWYtYzZlODY3ZTk3OTFlXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Rating = 9.0f,
                    Episodes = 1,
                    EpisodeDuration = 189,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1980, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Ongoing,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg",
                },
                new Movie
                {
                    Title = "12th Fail",
                    Rating = 9.0f,
                    Episodes = 1,
                    EpisodeDuration = 152,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2014, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://static.india.com/wp-content/uploads/2024/01/12-fail.jpg?impolicy=Medium_Widthonly&w=400&h=800",
                },
                new Movie
                {
                    Title = "Schindler's List",
                    Rating = 9.0f,
                    Episodes = 1,
                    EpisodeDuration = 112,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Ongoing,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNDE4OTMxMTctNmRhYy00NWE2LTg3YzItYTk3M2UwOTU5Njg4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Return of the King",
                    Rating = 8.8f,
                    Episodes = 1,
                    EpisodeDuration = 192,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2010, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Ongoing,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNzA5ZDNlZWMtM2NhNS00NDJjLTk4NDItYTRmY2EwMWZlMTY3XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "The Godfather Part II",
                    Rating = 8.8f,
                    Episodes = 1,
                    EpisodeDuration = 135,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1996, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMWMwMGQzZTItY2JlNC00OWZiLWIyMDctNDk2ZDQ2YjRjMWQ0XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "12 Angry Men",
                    Rating = 8.8f,
                    Episodes = 1,
                    EpisodeDuration = 165,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1995, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTWfdIqlGuZenOYji0I6kbs4TB0_zNyMfoUevw5Ly7Gbw&s",
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    Rating = 8.7f,
                    Episodes = 1,
                    EpisodeDuration = 190,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2007, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Ongoing,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNGNhMDIzZTUtNTBlZi00MTRlLWFjM2ItYzViMjE3YzI5MjljXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Fellowship of the Ring",
                    Rating = 8.7f,
                    Episodes = 1,
                    EpisodeDuration = 100,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2006, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Ongoing,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BN2EyZjM3NzUtNWUzMi00MTgxLWI0NTctMzY4M2VlOTdjZWRiXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Dune: Part Two",
                    Rating = 8.5f,
                    Episodes = 1,
                    EpisodeDuration = 150,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2021, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BN2QyZGU4ZDctOWMzMy00NTc5LThlOGQtODhmNDI1NmY5YzAwXkEyXkFqcGdeQXVyMDM2NDM2MQ@@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Inception",
                    Rating = 7.9f,
                    Episodes = 1,
                    EpisodeDuration = 156,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2000, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Fight Club",
                    Rating = 9.0f,
                    Episodes = 1,
                    EpisodeDuration = 156,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2003, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Rating = 9.1f,
                    Episodes = 1,
                    EpisodeDuration = 154,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2015, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/ru/d/de/%D0%A4%D0%BE%D1%80%D1%80%D0%B5%D1%81%D1%82_%D0%93%D0%B0%D0%BC%D0%BF.jpg",
                },
                new Movie
                {
                    Title = "The Lord of the Rings: The Two Towers",
                    Rating = 9.6f,
                    Episodes = 1,
                    EpisodeDuration = 134,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2008, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZTUxNzg3MDUtYjdmZi00ZDY1LTkyYTgtODlmOGY5N2RjYWUyXkEyXkFqcGdeQXVyMTA0MTM5NjI2._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "The Good, the Bad and the Ugly",
                    Rating = 7.9f,
                    Episodes = 1,
                    EpisodeDuration = 141,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2005, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/4/45/Good_the_bad_and_the_ugly_poster.jpg",
                },
                new Movie
                {
                    Title = "Interstellar",
                    Rating = 8.7f,
                    Episodes = 1,
                    EpisodeDuration = 147,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2001, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZjdkOTU3MDktN2IxOS00OGEyLWFmMjktY2FiMmZkNWIyODZiXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                },
                new Movie
                {
                    Title = "The Matrix",
                    Rating = 9.5f,
                    Episodes = 1,
                    EpisodeDuration = 148,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1997, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNzQzOTk3OTAtNDQ0Zi00ZTVkLWI0MTEtMDllZjNkYzNjNTc4L2ltYWdlXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Goodfellas",
                    Rating = 9.6f,
                    Episodes = 1,
                    EpisodeDuration = 124,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2006, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BY2NkZjEzMDgtN2RjYy00YzM1LWI4ZmQtMjIwYjFjNmI3ZGEwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "One Flew Over the Cuckoo's Nest",
                    Rating = 9.1f,
                    Episodes = 1,
                    EpisodeDuration = 174,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2006, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/en/2/26/One_Flew_Over_the_Cuckoo%27s_Nest_poster.jpg",
                },
                new Movie
                {
                    Title = "Star Wars: Episode V - The Empire Strikes Back",
                    Rating = 9.2f,
                    Episodes = 1,
                    EpisodeDuration = 153,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2020, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYmU1NDRjNDgtMzhiMi00NjZmLTg5NGItZDNiZjU5NTU4OTE0XkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Spider-Man: Across the Spider-Verse",
                    Rating = 8.3f,
                    Episodes = 1,
                    EpisodeDuration = 153,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2008, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzI0NmVkMjEtYmY4MS00ZDMxLTlkZmEtMzU4MDQxYTMzMjU2XkEyXkFqcGdeQXVyMzQ0MzA0NTM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Se7en",
                    Rating = 7.3f,
                    Episodes = 1,
                    EpisodeDuration = 164,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2009, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTUwODM5MTctZjczMi00OTk4LTg3NWUtNmVhMTAzNTNjYjcyXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "The Silence of the Lambs",
                    Rating = 9.3f,
                    Episodes = 1,
                    EpisodeDuration = 187,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Ongoing,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNjNhZTk0ZmEtNjJhMi00YzFlLWE1MmEtYzM1M2ZmMGMwMTU4XkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Spirited Away",
                    Rating = 6.3f,
                    Episodes = 1,
                    EpisodeDuration = 134,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2009, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjlmZmI5MDctNDE2YS00YWE0LWE5ZWItZDBhYWQ0NTcxNWRhXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Star Wars: Episode IV - A New Hope",
                    Rating = 7.3f,
                    Episodes = 1,
                    EpisodeDuration = 153,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BOTA5NjhiOTAtZWM0ZC00MWNhLThiMzEtZDFkOTk2OTU1ZDJkXkEyXkFqcGdeQXVyMTA4NDI1NTQx._V1_.jpg",
                },
                new Movie
                {
                    Title = "The Green Mile",
                    Rating = 8.2f,
                    Episodes = 1,
                    EpisodeDuration = 192,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2018, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTUxMzQyNjA5MF5BMl5BanBnXkFtZTYwOTU2NTY3._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "Terminator 2: Judgment Day",
                    Rating = 8.1f,
                    Episodes = 1,
                    EpisodeDuration = 100,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1994, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMGU2NzRmZjUtOGUxYS00ZjdjLWEwZWItY2NlM2JhNjkxNTFmXkEyXkFqcGdeQXVyNjU0OTQ0OTY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Saving Private Ryan",
                    Rating = 8.0f,
                    Episodes = 1,
                    EpisodeDuration = 136,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2018, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZjhkMDM4MWItZTVjOC00ZDRhLThmYTAtM2I5NzBmNmNlMzI1XkEyXkFqcGdeQXVyNDYyMDk5MTU@._V1_.jpg",
                },
                new Movie
                {
                    Title = "City of God",
                    Rating = 7.8f,
                    Episodes = 1,
                    EpisodeDuration = 184,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2016, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Upcoming,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMGU5OWEwZDItNmNkMC00NzZmLTk1YTctNzVhZTJjM2NlZTVmXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "Life Is Beautiful",
                    Rating = 6.8f,
                    Episodes = 1,
                    EpisodeDuration = 157,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2013, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYmJmM2Q4NmMtYThmNC00ZjRlLWEyZmItZTIwOTBlZDQ3NTQ1XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Seven Samurai",
                    Rating = 9.3f,
                    Episodes = 1,
                    EpisodeDuration = 165,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2013, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BOGNiY2QzOTAtOWNlZC00NjAwLWFmMTAtNTJhYTBiNTY4ODU0XkEyXkFqcGdeQXVyMTQyMTMwOTk0._V1_.jpg",
                },
                new Movie
                {
                    Title = "It's a Wonderful Life",
                    Rating = 9.1f,
                    Episodes = 1,
                    EpisodeDuration = 154,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1989, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://upload.wikimedia.org/wikipedia/commons/2/25/It%27s_a_Wonderful_Life_%281946_poster%29.jpeg",
                },
                new Movie
                {
                    Title = "Harakiri",
                    Rating = 9.7f,
                    Episodes = 1,
                    EpisodeDuration = 176,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2005, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMWNmYzg5NWYtNzhiMS00NzlkLTgyOTUtYzM3MjY0NDc2YjFkXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Alien",
                    Rating = 7.8f,
                    Episodes = 1,
                    EpisodeDuration = 165,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2005, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BOGQzZTBjMjQtOTVmMS00NGE5LWEyYmMtOGQ1ZGZjNmRkYjFhXkEyXkFqcGdeQXVyMjUzOTY1NTc@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Gladiator",
                    Rating = 8.8f,
                    Episodes = 1,
                    EpisodeDuration = 175,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2005, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/I/61MSIFHAxML._AC_UF894,1000_QL80_.jpg",
                },
                new Movie
                {
                    Title = "The Departed",
                    Rating = 9.8f,
                    Episodes = 1,
                    EpisodeDuration = 142,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(2006, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/p162564_p_v8_ag.jpg",
                },
                new Movie
                {
                    Title = "Parasite",
                    Rating = 6.9f,
                    Episodes = 1,
                    EpisodeDuration = 165,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1997, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYWZjMjk3ZTItODQ2ZC00NTY5LWE0ZDYtZTI3MjcwN2Q5NTVkXkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_.jpg",
                },
                new Movie
                {
                    Title = "The Prestige",
                    Rating = 7.2f,
                    Episodes = 1,
                    EpisodeDuration = 165,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1991, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjA4NDI0MTIxNF5BMl5BanBnXkFtZTYwNTM0MzY2._V1_.jpg",
                },
                new Movie
                {
                    Title = "Back to the Future",
                    Rating = 8.3f,
                    Episodes = 1,
                    EpisodeDuration = 188,
                    MovieType = Domain.Enums.MovieType.Movie,
                    ReleaseDate = new DateTime(1992, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZmU0M2Y1OGUtZjIxNi00ZjBkLTg1MjgtOWIyNThiZWIwYjRiXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Breaking Bad",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2008, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BYmQ4YWMxYjUtNjZmYi00MDQ1LWFjMjMtNjA5ZDdiYjdiODU5XkEyXkFqcGdeQXVyMTMzNDExODE5._V1_.jpg",
                },
                new Movie
                {
                    Title = "Planet Earth II",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2018, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMGZmYmQ5NGQtNWQ1MC00NWZlLTg0MjYtYjJjMzQ5ODgxYzRkXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "Planet Earth",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2016, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMzMyYjg0MGMtMTlkMy00OGFiLWFiNTYtYmFmZWNmMDFlMzkwXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Chernobyl",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMmI2OTMxZTUtMTM5OS00MGNiLWFhNGMtYzJiODUwYjRmODA5XkEyXkFqcGdeQXVyMTM0NTc2NDgw._V1_.jpg",
                },
                new Movie
                {
                    Title = "Hidden Love",
                    Rating = 9.9f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2019, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BZmI5M2E1ODYtNTgxMy00Y2U4LTk0OGItNTViNGM3Yzk0NzdkXkEyXkFqcGdeQXVyMjI2ODE1NTA@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Tale of the Nine Tailed",
                    Rating = 9.8f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2020, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BODNmY2ZmNTMtYWZiNS00NTc3LWI0NzMtMGRkMGMwMDM3NDRkXkEyXkFqcGdeQXVyNDU4MDQ0MjM@._V1_.jpg",
                },
                new Movie
                {
                    Title = "All of Us Are Dead",
                    Rating = 9.1f,
                    Episodes = 16,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2022, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://suarausu.or.id/wp-content/uploads/2022/02/WhatsApp-Image-2022-02-18-at-23.34.22.jpeg",
                },
                new Movie
                {
                    Title = "Game of Thrones",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2004, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BN2IzYzBiOTQtNGZmMi00NDI5LTgxMzMtN2EzZjA1NjhlOGMxXkEyXkFqcGdeQXVyNjAwNDUxODI@._V1_FMjpg_UX1000_.jpg",
                },
                new Movie
                {
                    Title = "Sherlock",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2010, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMWEzNTFlMTQtMzhjOS00MzQ1LWJjNjgtY2RhMjFhYjQwYjIzXkEyXkFqcGdeQXVyNDIzMzcwNjc@._V1_.jpg",
                },
                new Movie
                {
                    Title = "Attack on Titan",
                    Rating = 9.1f,
                    Episodes = 24,
                    EpisodeDuration = 60,
                    MovieType = Domain.Enums.MovieType.Tv,
                    ReleaseDate = new DateTime(2015, 1, 1),
                    MovieStatus = Domain.Enums.MovieStatus.Finished,
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BNDFjYTIxMjctYTQ2ZC00OGQ4LWE3OGYtNDdiMzNiNDZlMDAwXkEyXkFqcGdeQXVyNzI3NjY3NjQ@._V1_FMjpg_UX1000_.jpg",
                },
            };

            foreach (Movie movie in movies)
            {
                var response = await tmdbService.GetMediaAsync(movie.MovieType, movie.Title);

                var tmdbMovie = response.Results.FirstOrDefault();

                movie.TmdbId = tmdbMovie?.Id;
                movie.TmdbRating = tmdbMovie?.VoteAverage;
                movie.Description = tmdbMovie?.Overview;

                context.Movies.Add(movie);
            }

            context.SaveChanges();

            #endregion Movie

            #region Genres

            var genres = new Genre[]
            {
                new Genre{ Name = "Romance" },
                new Genre{ Name = "Action" },
                new Genre{ Name = "Drama" },
                new Genre{ Name = "Military" },
                new Genre{ Name = "Magic" },
                new Genre{ Name = "Comedy" },
                new Genre{ Name = "History" },
                new Genre{ Name = "Psychological" },
                new Genre{ Name = "Crime" },
                new Genre{ Name = "Adventure" },
                new Genre{ Name = "Sci-Fi" },
                new Genre{ Name = "Fantasy" },
                new Genre{ Name = "Thriller" },
                new Genre{ Name = "Animation" },
                new Genre{ Name = "Horror" },
                new Genre{ Name = "Documentary" },
            };

            foreach (Genre genre in genres)
            {
                context.Genres.Add(genre);
            }

            context.SaveChanges();

            #endregion Genres

            #region MovieGenres

            var movieGenres = new MovieGenre[]
            {
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Shawshank Redemption").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Godfather").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Godfather").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Dark Knight").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Dark Knight").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Dark Knight").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "12th Fail").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Schindler's List").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Schindler's List").Id,
                    GenreId = genres.Single(x => x.Name == "History").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Return of the King").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Return of the King").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Return of the King").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Godfather Part II").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Godfather Part II").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "12 Angry Men").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "12 Angry Men").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                 new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Pulp Fiction").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Pulp Fiction").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Fellowship of the Ring").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Fellowship of the Ring").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Fellowship of the Ring").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Dune: Part Two").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Dune: Part Two").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Dune: Part Two").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Inception").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Inception").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Fight Club").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Forrest Gump").Id,
                    GenreId = genres.Single(x => x.Name == "Romance").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Forrest Gump").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Two Towers").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Two Towers").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Lord of the Rings: The Two Towers").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Good, the Bad and the Ugly").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Interstellar").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Interstellar").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Interstellar").Id,
                    GenreId = genres.Single(x => x.Name == "Sci-Fi").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Matrix").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Matrix").Id,
                    GenreId = genres.Single(x => x.Name == "Sci-Fi").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Goodfellas").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Goodfellas").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "One Flew Over the Cuckoo's Nest").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Star Wars: Episode V - The Empire Strikes Back").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Star Wars: Episode V - The Empire Strikes Back").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Star Wars: Episode V - The Empire Strikes Back").Id,
                    GenreId = genres.Single(x => x.Name == "Fantasy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Spider-Man: Across the Spider-Verse").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Spider-Man: Across the Spider-Verse").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Se7en").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Se7en").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Silence of the Lambs").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Silence of the Lambs").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Silence of the Lambs").Id,
                    GenreId = genres.Single(x => x.Name == "Thriller").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Spirited Away").Id,
                    GenreId = genres.Single(x => x.Name == "Animation").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Spirited Away").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Star Wars: Episode IV - A New Hope").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Star Wars: Episode IV - A New Hope").Id,
                    GenreId = genres.Single(x => x.Name == "Fantasy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Star Wars: Episode IV - A New Hope").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Green Mile").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Green Mile").Id,
                    GenreId = genres.Single(x => x.Name == "Fantasy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Green Mile").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                 new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Terminator 2: Judgment Day").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Terminator 2: Judgment Day").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Terminator 2: Judgment Day").Id,
                    GenreId = genres.Single(x => x.Name == "Sci-Fi").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Saving Private Ryan").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Saving Private Ryan").Id,
                    GenreId = genres.Single(x => x.Name == "Military").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "City of God").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "City of God").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Life Is Beautiful").Id,
                    GenreId = genres.Single(x => x.Name == "Romance").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Life Is Beautiful").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Life Is Beautiful").Id,
                    GenreId = genres.Single(x => x.Name == "Comedy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Seven Samurai").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Seven Samurai").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "It's a Wonderful Life").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "It's a Wonderful Life").Id,
                    GenreId = genres.Single(x => x.Name == "Fantasy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Harakiri").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Harakiri").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Alien").Id,
                    GenreId = genres.Single(x => x.Name == "Horror").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Alien").Id,
                    GenreId = genres.Single(x => x.Name == "Sci-Fi").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Gladiator").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Gladiator").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Gladiator").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Departed").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Departed").Id,
                    GenreId = genres.Single(x => x.Name == "Thriller").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Departed").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Parasite").Id,
                    GenreId = genres.Single(x => x.Name == "Thriller").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Parasite").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Prestige").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "The Prestige").Id,
                    GenreId = genres.Single(x => x.Name == "Sci-Fi").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Back to the Future").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Back to the Future").Id,
                    GenreId = genres.Single(x => x.Name == "Comedy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Breaking Bad").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Breaking Bad").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Breaking Bad").Id,
                    GenreId = genres.Single(x => x.Name == "Thriller").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Planet Earth II").Id,
                    GenreId = genres.Single(x => x.Name == "Documentary").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Planet Earth").Id,
                    GenreId = genres.Single(x => x.Name == "Documentary").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Chernobyl").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Chernobyl").Id,
                    GenreId = genres.Single(x => x.Name == "History").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Chernobyl").Id,
                    GenreId = genres.Single(x => x.Name == "Thriller").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Game of Thrones").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Game of Thrones").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Game of Thrones").Id,
                    GenreId = genres.Single(x => x.Name == "Adventure").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Sherlock").Id,
                    GenreId = genres.Single(x => x.Name == "Crime").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Sherlock").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Sherlock").Id,
                    GenreId = genres.Single(x => x.Name == "Thriller").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Hidden Love").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Hidden Love").Id,
                    GenreId = genres.Single(x => x.Name == "Romance").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Hidden Love").Id,
                    GenreId = genres.Single(x => x.Name == "Comedy").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Attack on Titan").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Attack on Titan").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "All of Us Are Dead").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "All of Us Are Dead").Id,
                    GenreId = genres.Single(x => x.Name == "Horror").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Tale of the Nine Tailed").Id,
                    GenreId = genres.Single(x => x.Name == "Drama").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Tale of the Nine Tailed").Id,
                    GenreId = genres.Single(x => x.Name == "Horror").Id
                },
                new MovieGenre
                {
                    MovieId = movies.Single(x => x.Title == "Tale of the Nine Tailed").Id,
                    GenreId = genres.Single(x => x.Name == "Action").Id
                },
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

            #endregion MovieGenre
        }
    }
}
