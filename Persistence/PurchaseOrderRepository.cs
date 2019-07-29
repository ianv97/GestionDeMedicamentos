using GestionDeMedicamentos.Controllers;
using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public class PurchaseOrderRepository : BaseRepository, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<PaginatedList<PurchaseOrder>> ListAsync(string date, string order, int? pageNumber, int? pageSize)
        {
            var purchaseOrders = _context.PurchaseOrders.Where(po => (date == null || po.Date.ToString().StartsWith(date)));

            bool descending = false;
            if (order != null)
            {
                order = order.Substring(0, 1).ToUpper() + order.Substring(1, order.Length - 1);
                if (order.EndsWith("_desc"))
                {
                    order = order.Substring(0, order.Length - 5);
                    descending = true;
                }

                if (descending)
                {
                    purchaseOrders = purchaseOrders.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    purchaseOrders = purchaseOrders.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await PaginatedList<PurchaseOrder>.CreateAsync(purchaseOrders, pageNumber ?? 1, pageSize ?? 0);
        }

        public async Task<PurchaseOrder> FindAsync(int id)
        {
            return await _context.PurchaseOrders.Include(po => po.MedicinePurchaseOrders).ThenInclude(mpo => mpo.Medicine).FirstOrDefaultAsync(po => po.Id == id);
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

        public bool PurchaseOrderExists(int id)
        {
            return _context.Drugs.Any(e => e.Id == id);
        }
    }
}