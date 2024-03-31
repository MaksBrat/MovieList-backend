using MovieList.Domain.Chat;
using MovieList.Domain.Entity;
using MovieList.Domain.Entity.Account;
using MovieList.Domain.Entity.MovieNews;
using MovieList.Domain.Entity.Movies;
using MovieList.Domain.Entity.Genres;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieList.Domain.Entity.MovieList;
using MovieList.Domain.Entity.Profile;

namespace MovieList.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }   
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<MovieListItem> MovieListItems { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FileModel> FileModels { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User)
                .HasForeignKey<UserProfile>(x => x.UserId);

            modelBuilder.Entity<UserProfile>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.UserId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<UserProfile>()
                .HasMany(x => x.MovieList)
                .WithOne(x => x.Profile)
                .IsRequired();

            modelBuilder.Entity<FileModel>()
                .HasOne(x => x.UserProfile)
                .WithOne(x => x.FileModel)
                .HasForeignKey<UserProfile>(x => x.FileModelId);

            modelBuilder.Entity<MovieGenre>()
                .HasKey(ag => new { ag.MovieId, ag.GenreId });
            modelBuilder.Entity<MovieGenre>()
                .HasOne(ag => ag.Movie)
                .WithMany(x => x.MovieGenres)
                .HasForeignKey(x => x.MovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(x => x.MovieGenre)
                .HasForeignKey(x => x.GenreId);

            modelBuilder.Entity<Movie>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Title).IsRequired();
                builder.Property(x => x.MovieType).IsRequired();
            });

            modelBuilder.Entity<MovieListItem>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<News>(builder =>
            {
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<News>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.News)
                .HasForeignKey(x => x.NewsId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.News)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.NewsId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
