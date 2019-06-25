using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Domain
{
    public interface IStockOrderRepository
    {
        Task<IEnumerable<StockOrder>> ListAsync(string date, string order);
        Task<StockOrder> FindAsync(int id);
        Task<EntityEntry> CreateAsync(StockOrder stockOrder);
        EntityEntry Delete(StockOrder stockOrder);
        Task SaveChangesAsync();
        bool StockOrderExists(int id);
    }
}
