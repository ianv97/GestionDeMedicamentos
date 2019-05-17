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
    public class PrescriptionRepository : BaseRepository, IPrescriptionRepository
    {
        public PrescriptionRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Prescription>> ListAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }
    }
}
