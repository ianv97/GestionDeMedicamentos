using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Persistence
{
    public class MedicinePurchaseOrderRepository : BaseRepository, IMedicinePurchaseOrderRepository
    {
        public MedicinePurchaseOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicinePurchaseOrder>> ListAsync()
        {
            return await _context.MedicinePurchaseOrders.ToListAsync();
        }

        public async Task<MedicinePurchaseOrder> FindAsync(int id)
        {
            return await _context.MedicinePurchaseOrders.FindAsync(id);
        }

        public EntityState Update(MedicinePurchaseOrder medicinePurchaseOrder)
        {
            return _context.Entry(medicinePurchaseOrder).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(MedicinePurchaseOrder medicinePurchaseOrder)
        {
            return await _context.MedicinePurchaseOrders.AddAsync(medicinePurchaseOrder);
        }

        public EntityEntry Delete(MedicinePurchaseOrder medicinePurchaseOrder)
        {
            return _context.MedicinePurchaseOrders.Remove(medicinePurchaseOrder);
        }

        public bool MedicinePurchaseOrderExists(int id)
        {
            return _context.MedicinePurchaseOrders.Any(e => e.Id == id);
        }
    }
}

