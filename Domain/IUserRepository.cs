using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Domain
{
    public interface IUserRepository
    {
        Task<PaginatedList<User>> ListAsync(string username, string name, string order, int? pageNumber, int? pageSize);

        Task<User> Login(string username, string password);

        Task<User> FindAsync(int id);

        Task<User> FindByUsername(string username);

        EntityState Update(User user);

        Task<EntityEntry> CreateAsync(User user);

        EntityEntry Delete(User user);

        Task SaveChangesAsync();

        bool UserExists(int id);
    }
}
