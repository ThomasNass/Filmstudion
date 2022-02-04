using Filmstudion.API.Persistence.Contexts;

namespace Filmstudion.API.Persistence.Repositories
{
        public abstract class BaseRepository
        {
            protected readonly AppDbContext _context;

            public BaseRepository(AppDbContext context)
            {
                _context = context;
            }
        }   
}
