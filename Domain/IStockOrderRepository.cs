using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Domain
{
    public interface IStockOrderRepository
    {
        Task<PaginatedList<StockOrder>> ListAsync(string date, string order, int? pageNumber, int? pageSize);
        Task<StockOrder> FindAsync(int id);
        Task<EntityEntry> CreateAsync(StockOrder stockOrder);
        EntityEntry Delete(StockOrder stockOrder);
        Task SaveChangesAsync();
        bool StockOrderExists(int id);
    }
}
