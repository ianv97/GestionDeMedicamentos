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
    public class DrugRepository : BaseRepository, IDrugRepository
    {
        public DrugRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Drug>> ListAsync()
        {
            return await _context.Drugs.ToListAsync();
        }

        public async Task<Drug> FindAsync(int id)
        {
            return await _context.Drugs.FindAsync(id);
        }

        public async Task<IEnumerable<Drug>> FindAsyncByName(string name)
        {
            return await _context.Drugs.Where(d => d.Name.StartsWith(name)).ToListAsync();
        }

        public EntityState Update(Drug drug)
        {
            return _context.Entry(drug).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(Drug drug)
        {
            return await _context.Drugs.AddAsync(drug);
        }

        public EntityEntry Delete(Drug drug)
        {
            return _context.Drugs.Remove(drug);
        }

        public bool DrugExists(int id)
        {
            return _context.Drugs.Any(e => e.Id == id);
        }
    }
}
