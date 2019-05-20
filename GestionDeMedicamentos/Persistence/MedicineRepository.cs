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
    public class MedicineRepository : BaseRepository, IMedicineRepository
    {
        public MedicineRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Medicine>> ListAsync()
        {
            return await _context.Medicines.Include(m => m.Drug).ToListAsync();
        }

        public async Task<Medicine> FindAsync(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<IEnumerable<Medicine>> FindAsyncByName(string name)
        {
            return await _context.Medicines.Where(d => d.Name.StartsWith(name)).ToListAsync();
        }

        public EntityState Update(Medicine medicine)
        {
            return _context.Entry(medicine).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(Medicine medicine)
        {
            return await _context.Medicines.AddAsync(medicine);
        }

        public EntityEntry Delete(Medicine medicine)
        {
            return _context.Medicines.Remove(medicine);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
    }
}
