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
    public class MedicineStockOrderRepository : BaseRepository, IMedicineStockOrderRepository
    {
        public MedicineStockOrderRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MedicineStockOrder>> ListAsync()
        {
            return await _context.MedicineStockOrders.Include(medstock => medstock.Medicine).Include(medstock => medstock.StockOrder).ToListAsync();
        }

        public async Task<MedicineStockOrder> FindAsync(int id)
        {
            return await _context.MedicineStockOrders.Include(medstock => medstock.Medicine).Include(medstock => medstock.StockOrder).FirstOrDefaultAsync(medstock => medstock.Id == id);
        }


        public EntityState Update(MedicineStockOrder medicineStockOrder)
        {
            return _context.Entry(medicineStockOrder).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(MedicineStockOrder medicineStockOrder)
        {
            return await _context.MedicineStockOrders.AddAsync(medicineStockOrder);
        }

        public EntityEntry Delete(MedicineStockOrder medicineStockOrder)
        {
            return _context.MedicineStockOrders.Remove(medicineStockOrder);
        }

        public bool MedicineStockOrderExists(int id)
        {
            return _context.MedicineStockOrders.Any(e => e.Id == id);
        }
    }
}