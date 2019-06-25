using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> ListAsync(string date, string order);
        Task<PurchaseOrder> FindAsync(int id);
        Task<EntityEntry> CreateAsync(PurchaseOrder purchaseOrder);
        EntityEntry Delete(PurchaseOrder purchaseOrder);
        Task SaveChangesAsync();
        bool PurchaseOrderExists(int id);
    }
}
