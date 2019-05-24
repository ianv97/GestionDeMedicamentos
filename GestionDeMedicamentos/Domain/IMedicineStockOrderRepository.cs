using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IMedicineStockOrderRepository
    {
        Task<IEnumerable<MedicineStockOrder>> ListAsync();
        Task<MedicineStockOrder> FindAsync(int id);
        EntityState Update(MedicineStockOrder medicineStockOrder);
        Task<EntityEntry> CreateAsync(MedicineStockOrder medicineStockOrder);
        EntityEntry Delete(MedicineStockOrder medicineStockOrder);
        Task SaveChangesAsync();
        bool MedicineStockOrderExists(int id);
    }
}