using Filmstudion.API.Models.Film;
using Filmstudion.API.Persistence.Contexts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
            var filmen = _context.Films.Add(film);
            _context.SaveChanges();
        }

        public void Update(Film film)
        {
           
            _context.Films.Update(film);
            _context.SaveChanges();
            
        }

        public void CreateCopy(FilmCopy filmCopy)
        {
            _context.FilmCopies.Add(filmCopy);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<FilmCopy>> GetFilmCopies()
        {
            return await _context.FilmCopies.ToListAsync();
        }

        public async Task DeleteFilmCopy(int filmId)
        {
            var copy = await _context.FilmCopies.LastOrDefaultAsync(c => c.FilmId == filmId && c.RentedOut != true);
            _context.FilmCopies.Remove(copy);
            _context.SaveChanges();
        }

        public void UpdateFilmCopy(FilmCopy filmCopy)
        {
            var copy =  _context.FilmCopies.FirstOrDefault(c => c.FilmCopyId == filmCopy.FilmCopyId);
            _context.FilmCopies.Update(copy);
            _context.SaveChanges();
        }
    }
}
