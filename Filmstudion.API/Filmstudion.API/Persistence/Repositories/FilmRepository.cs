using Filmstudion.API.Models.Film;
using Filmstudion.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Persistence.Repositories
{
    public class FilmRepository: BaseRepository, IFilmRepository
    {
        public FilmRepository (AppDbContext context): base(context)
        { }

        public async Task<IEnumerable<Film>> ListAsync()
        {
            return await _context.Films.ToListAsync();
        }

        public void Create(Film film)
        {
            _context.Films.Add(film);
            _context.SaveChanges();
        }
    }
}
