using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Persistence
{
    public class StockOrderRepository : BaseRepository, IStockOrderRepository
    {
        public StockOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StockOrder>> ListAsync(string date, string order)
        {
            var stockOrders = _context.StockOrders.Where(so => (date == null || so.Date.ToString().StartsWith(date)));

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
                    stockOrders = stockOrders.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    stockOrders = stockOrders.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await stockOrders.ToListAsync();
        }

        public async Task<StockOrder> FindAsync(int id)
        {
            return await _context.StockOrders.Include(so => so.MedicineStockOrders).FirstOrDefaultAsync(so => so.Id == id);
        }


        public EntityState Update(StockOrder stockOrder)
        {
            return _context.Entry(stockOrder).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(StockOrder stockOrder)
        {
            return await _context.StockOrders.AddAsync(stockOrder);
        }

        public EntityEntry Delete(StockOrder stockOrder)
        {
            return _context.StockOrders.Remove(stockOrder);
        }

        public bool StockOrderExists(int id)
        {
            return _context.StockOrders.Any(e => e.Id == id);
        }
    }
}
