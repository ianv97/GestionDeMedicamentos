using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<MedicinePrescription> FindAsync(int id)
        {
            return await _context.MedicinePrescriptions.FindAsync(id);
        }

        public EntityState Update(MedicinePrescription medicinePrescription)
        {
            return _context.Entry(medicinePrescription).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(MedicinePrescription medicinePrescription)
        {
            return await _context.MedicinePrescriptions.AddAsync(medicinePrescription);
        }

        public EntityEntry Delete(MedicinePrescription medicinePrescription)
        {
            return _context.MedicinePrescriptions.Remove(medicinePrescription);
        }

        public bool MedicinePrescriptionExists(int id)
        {
            return _context.MedicinePrescriptions.Any(e => e.Id == id);
        }
    }
}
