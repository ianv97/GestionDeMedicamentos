using GestiónDeMedicamentos.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public abstract class BaseRepository
    {
        protected readonly PostgreContext _context;

        public BaseRepository(PostgreContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
