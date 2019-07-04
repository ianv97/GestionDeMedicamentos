using GestionDeMedicamentos.Controllers;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;


namespace GestiónDeMedicamentos.Domain
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
