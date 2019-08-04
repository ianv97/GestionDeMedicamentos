using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;


namespace GestionDeMedicamentos.Domain
{
    public interface IPrescriptionRepository
    {
        Task<PaginatedList<Prescription>> ListAsync(string date, string order, int? pageNumber, int? pageSize);
        Task<Prescription> FindAsync(int id);
        Task<EntityEntry> CreateAsync(Prescription prescription);
        EntityEntry Delete(Prescription prescription);
        Task SaveChangesAsync();
        bool PrescriptionExists(int id);
    }
}
