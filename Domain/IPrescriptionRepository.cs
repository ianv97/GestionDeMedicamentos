using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GestiónDeMedicamentos.Domain
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> ListAsync(string date, string order);
        Task<Prescription> FindAsync(int id);
        Task<EntityEntry> CreateAsync(Prescription prescription);
        EntityEntry Delete(Prescription prescription);
        Task SaveChangesAsync();
        bool PrescriptionExists(int id);
    }
}
