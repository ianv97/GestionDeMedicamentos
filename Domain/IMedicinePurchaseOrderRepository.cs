using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
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
        Task SaveChangesAsync();
        bool MedicinePurchaseOrderExists(int id);
    }
}
