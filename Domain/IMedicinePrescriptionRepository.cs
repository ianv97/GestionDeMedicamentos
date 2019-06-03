using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IMedicinePrescriptionRepository
    {
        Task<IEnumerable<MedicinePrescription>> ListAsync();
        Task<MedicinePrescription> FindAsync(int id);
        EntityState Update(MedicinePrescription medicinePrescription);
        Task<EntityEntry> CreateAsync(MedicinePrescription medicinePrescription);
        EntityEntry Delete(MedicinePrescription medicinePrescription);
        Task SaveChangesAsync();
        bool MedicinePrescriptionExists(int id);
    }
}
