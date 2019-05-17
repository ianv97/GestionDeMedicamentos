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
    public class MedicineStockOrderRepository : BaseRepository, IMedicineStockOrderRepository
    {
        public MedicineStockOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicineStockOrder>> ListAsync()
        {
            return await _context.MedicineStockOrders.ToListAsync();
        }
    }
}
