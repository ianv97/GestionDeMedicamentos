using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public class MedicineRepository : BaseRepository, IMedicineRepository
    {
        public MedicineRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Medicine>> ListAsync()
        {
            return await _context.Medicines.ToListAsync();
        }
    }
}
