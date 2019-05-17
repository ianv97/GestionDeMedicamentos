using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    interface IMedicinePurchaseOrderRepository
    {
        Task<IEnumerable<MedicinePurchaseOrder>> ListAsync();
        Task<MedicinePurchaseOrder> FindAsync(int id);
        EntityState Update(MedicinePurchaseOrder medicineMedicinePurchaseOrder);
        Task<EntityEntry> CreateAsync(MedicinePurchaseOrder medicineMedicinePurchaseOrder);
        EntityEntry Delete(MedicinePurchaseOrder medicineMedicinePurchaseOrder);
        Task<int> SaveChangesAsync();
        bool MedicinePurchaseOrderExists(int id);
    }
}
