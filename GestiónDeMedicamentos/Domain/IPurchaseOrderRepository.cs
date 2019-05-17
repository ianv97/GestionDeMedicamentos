using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> ListAsync();
        Task<PurchaseOrder> FindAsync(int id);
        EntityState Update(PurchaseOrder purchaseOrder);
        Task<EntityEntry> CreateAsync(PurchaseOrder purchaseOrder);
        EntityEntry Delete(PurchaseOrder purchaseOrder);
        Task<int> SaveChangesAsync();
        bool PurchaseOrderExists(int id);
    }
}
