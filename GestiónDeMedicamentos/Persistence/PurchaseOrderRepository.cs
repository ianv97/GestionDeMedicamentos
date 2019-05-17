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
    public class PurchaseOrderRepository : BaseRepository, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PurchaseOrder>> ListAsync()
        {
            return await _context.PurchaseOrders.ToListAsync();
        }
    }
}
