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
    public class MedicinePrescriptionRepository : BaseRepository, IMedicinePrescriptionRepository
    {
        public MedicinePrescriptionRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicinePrescription>> ListAsync()
        {
            return await _context.MedicinePrescriptions.ToListAsync();
        }
    }
}
