using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public class PurchaseOrderRepository : BaseRepository, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PurchaseOrder>> ListAsync()
        {
            return await _context.PurchaseOrders.ToListAsync();
        }

        public async Task<PurchaseOrder> FindAsync(int id)
        {
            return await _context.PurchaseOrders.FindAsync(id);
        }


        public EntityState Update(PurchaseOrder purchaseOrder)
        {
            return _context.Entry(purchaseOrder).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(PurchaseOrder purchaseOrder)
        {
            return await _context.PurchaseOrders.AddAsync(purchaseOrder);
        }

        public EntityEntry Delete(PurchaseOrder purchaseOrder)
        {
            return _context.PurchaseOrders.Remove(purchaseOrder);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool PurchaseOrderExists(int id)
        {
            return _context.Drugs.Any(e => e.Id == id);
        }
    }
}