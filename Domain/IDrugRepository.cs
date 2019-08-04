using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Domain
{
    public interface IDrugRepository
    {
        Task<PaginatedList<Drug>> ListAsync(string name, string order, int? pageNumber, int? pageSize);
        Task<Drug> FindAsync(int id);
        EntityState Update(Drug drug);
        Task<EntityEntry> CreateAsync(Drug drug);
        EntityEntry Delete(Drug drug);
        Task SaveChangesAsync();
        bool DrugExists(int id);
    }
}
