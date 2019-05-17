using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Domain;
using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
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
    }
}
