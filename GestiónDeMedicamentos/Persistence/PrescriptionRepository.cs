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
    public class PrescriptionRepository : BaseRepository, IPrescriptionRepository
    {
        public PrescriptionRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Prescription>> ListAsync()
        {
            return await _context.Prescriptions.ToListAsync();
        }

        public async Task<Prescription> FindAsync(int id)
        {
            return await _context.Prescriptions.FindAsync(id);
        }


        public EntityState Update(Prescription prescription)
        {
            return _context.Entry(prescription).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(Prescription prescription)
        {
            return await _context.Prescriptions.AddAsync(prescription);
        }

        public EntityEntry Delete(Prescription prescription)
        {
            return _context.Prescriptions.Remove(prescription);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool PrescriptionExists(int id)
        {
            return _context.Prescriptions.Any(e => e.Id == id);
        }
    }
}
