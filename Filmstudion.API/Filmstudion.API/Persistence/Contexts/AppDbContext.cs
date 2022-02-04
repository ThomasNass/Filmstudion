using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudio;
using Filmstudion.API.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Filmstudion.API.Persistence.Contexts
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }
       // public override DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Film>().HasData(
                new Film { FilmId = 1, Description= "A movie", Name ="Movie 1", FilmCopies = 3 },
                new Film { FilmId = 2, Description = "A movie", Name = "Movie 2", FilmCopies = 3 },
                new Film { FilmId = 3, Description = "A movie", Name = "Movie 3", FilmCopies = 3 }

            );
        }
    }
}
