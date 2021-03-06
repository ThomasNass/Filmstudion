using Filmstudion.API.Models.Film;
using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Models.User;
using Microsoft.AspNetCore.Identity;
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

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "admin", NormalizedName = "admin".ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "filmstudio", NormalizedName = "filmstudio".ToUpper() });

            builder.Entity<Film>().HasData(
                new Film { FilmId = 1, Name = "Movie 1" ,Director ="Ingmar B",Country = "Sweden"},
                new Film { FilmId = 2, Name = "Movie 2", Director = "Ingmar B", Country = "Sweden" },
                new Film { FilmId = 3, Name = "Movie 3", Director = "Ingmar B", Country = "Sweden" }

            );
            builder.Entity<FilmCopy>().HasData(
                new FilmCopy { FilmCopyId = 1, FilmId = 1,RentedOut=false },
                new FilmCopy { FilmCopyId = 2, FilmId = 1, RentedOut = false },
                new FilmCopy { FilmCopyId = 3, FilmId = 1, RentedOut = false },
                new FilmCopy { FilmCopyId = 4, FilmId = 2, RentedOut = false },
                new FilmCopy { FilmCopyId = 5, FilmId = 2, RentedOut = false },
                new FilmCopy { FilmCopyId = 6, FilmId = 2, RentedOut = false },
                new FilmCopy { FilmCopyId = 7, FilmId = 3, RentedOut = false },
                new FilmCopy { FilmCopyId = 8, FilmId = 3, RentedOut = false },
                new FilmCopy { FilmCopyId = 9, FilmId = 3, RentedOut = false }
                );
        }
    }
}
