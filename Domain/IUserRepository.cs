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

        User Login(string username, string password);

        Task<User> FindAsync(int id);

        EntityState Update(User user);

        Task<EntityEntry> CreateAsync(User user);

        EntityEntry Delete(User user);

        bool UserExists(int id);
    }
}
