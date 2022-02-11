using Filmstudion.API.Models.FilmStudioDir;
using Filmstudion.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Persistence.Repositories
{
    public class FilmStudioRepository: BaseRepository
    {
        public FilmStudioRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<FilmStudio>> ListAsync()
        {
            return await _context.FilmStudios.ToListAsync();
        }

        public void Create(FilmStudio filmStudio)
        {
            _context.FilmStudios.Add(filmStudio);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
