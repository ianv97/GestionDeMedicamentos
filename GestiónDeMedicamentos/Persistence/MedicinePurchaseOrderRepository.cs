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
    public class MedicinePurchaseOrderRepository : BaseRepository, IMedicinePurchaseOrderRepository
    {
        public MedicinePurchaseOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicinePurchaseOrder>> ListAsync()
        {
            return await _context.MedicinePurchaseOrders.ToListAsync();
        }
    }
}
