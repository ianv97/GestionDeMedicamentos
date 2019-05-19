using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> ListAsync();
        Task<Medicine> FindAsync(int id);
        Task<IEnumerable<Medicine>> FindAsyncByName(string name);
        EntityState Update(Medicine medicine);
        Task<EntityEntry> CreateAsync(Medicine medicine);
        EntityEntry Delete(Medicine medicine);
        Task<int> SaveChangesAsync();
        bool MedicineExists(int id);
    }
}
