using GestionDeMedicamentos.Controllers;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IMedicineRepository
    {
        Task<PaginatedList<Medicine>> ListAsync(string name, string drug, string proportion, string presentation, string laboratory, string stock, string order, int? pageNumber, int? pageSize);
        Task<Medicine> FindAsync(int id);
        EntityState Update(Medicine medicine);
        Task<EntityEntry> CreateAsync(Medicine medicine);
        EntityEntry Delete(Medicine medicine);
        Task SaveChangesAsync();
        bool MedicineExists(int id);
    }
}
