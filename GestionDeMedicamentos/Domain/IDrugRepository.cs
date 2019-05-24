using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IDrugRepository
    {
        Task<IEnumerable<Drug>> ListAsync();
        Task<Drug> FindAsync(int id);
        Task<IEnumerable<Drug>> FindAsyncByName(string name);
        EntityState Update(Drug drug);
        Task<EntityEntry> CreateAsync(Drug drug);
        EntityEntry Delete(Drug drug);
        Task SaveChangesAsync();
        bool DrugExists(int id);
    }
}
