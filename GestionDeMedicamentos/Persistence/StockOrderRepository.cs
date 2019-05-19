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
    public class StockOrderRepository : BaseRepository, IStockOrderRepository
    {
        public StockOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StockOrder>> ListAsync()
        {
            return await _context.StockOrders.ToListAsync();
        }

        public async Task<StockOrder> FindAsync(int id)
        {
            return await _context.StockOrders.FindAsync(id);
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

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool StockOrderExists(int id)
        {
            return _context.StockOrders.Any(e => e.Id == id);
        }
    }
}
