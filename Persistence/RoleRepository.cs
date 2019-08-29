using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Persistence
{
    public interface IRoleRepository
    {
        Task<PaginatedList<Role>> ListAsync(string name, string order, int? pageNumber, int? pageSize);

        Task<Role> FindAsync(int id);

        Task<Role> FindByName(string name);

        EntityState Update(Role role);

        Task<EntityEntry> CreateAsync(Role role);

        EntityEntry Delete(Role role);

        Task SaveChangesAsync();

        bool RoleExists(int id);
    }


    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<PaginatedList<Role>> ListAsync(string name, string order, int? pageNumber, int? pageSize)
        {
            var roles = _context.Roles.Where(r => name == null || r.Name.ToLower().StartsWith(name.ToLower()));

            bool descending = false;
            if (order != null)
            {
                order = order.Substring(0, 1).ToUpper() + order.Substring(1, order.Length - 1);
                if (order.EndsWith("_desc"))
                {
                    order = order.Substring(0, order.Length - 5);
                    descending = true;
                }

                if (descending)
                {
                    roles = roles.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    roles = roles.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await PaginatedList<Role>.CreateAsync(roles, pageNumber ?? 1, pageSize ?? 0);
        }

        public async Task<Role> FindAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role> FindByName(string name)
        {
            return await _context.Roles.Where(r => r.Name.Equals(name)).FirstOrDefaultAsync();
        }

        public EntityState Update(Role role)
        {
            return _context.Entry(role).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(Role role)
        {
            return await _context.Roles.AddAsync(role);
        }

        public EntityEntry Delete(Role role)
        {
            return _context.Roles.Remove(role);
        }

        public bool RoleExists(int id)
        {
            return _context.Roles.Any(r => r.Id == id);
        }
    }
}
