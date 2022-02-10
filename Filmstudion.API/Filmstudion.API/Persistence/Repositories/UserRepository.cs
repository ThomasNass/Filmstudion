using Filmstudion.API.Models.User;
using Filmstudion.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Filmstudion.API.Persistence.Repositories
{
    public class UserRepository:BaseRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        { }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

    }
}
