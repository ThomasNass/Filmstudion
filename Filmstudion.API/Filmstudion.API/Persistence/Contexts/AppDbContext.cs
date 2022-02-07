using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Filmstudion.API.Persistence.Contexts
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set;}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Film>().HasData(
                new Film { FilmId = 1, Name = "Movie 1" },
                new Film { FilmId = 2, Name = "Movie 2"},
                new Film { FilmId = 3, Name = "Movie 3" }

            );
        }
    }
}
