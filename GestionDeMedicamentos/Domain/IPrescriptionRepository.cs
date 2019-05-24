using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GestiónDeMedicamentos.Domain
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> ListAsync();
        Task<Prescription> FindAsync(int id);
        EntityState Update(Prescription prescription);
        Task<EntityEntry> CreateAsync(Prescription prescription);
        Task<IEnumerable<Prescription>> FindAsyncByDate(DateTime date);
        EntityEntry Delete(Prescription prescription);
        Task SaveChangesAsync();
        bool PrescriptionExists(int id);


    }
}
