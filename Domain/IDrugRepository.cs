using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IDrugRepository
    {
        Task<IEnumerable<Drug>> ListAsync(string name, string order);
        Task<Drug> FindAsync(int id);
        EntityState Update(Drug drug);
        Task<EntityEntry> CreateAsync(Drug drug);
        EntityEntry Delete(Drug drug);
        Task SaveChangesAsync();
        bool DrugExists(int id);
    }
}
