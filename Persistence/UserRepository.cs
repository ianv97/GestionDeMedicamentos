using System;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GestionDeMedicamentos.Persistence
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


    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<PaginatedList<User>> ListAsync(string username, string name, string order, int? pageNumber, int? pageSize)
        {
            var users = _context.Users.Include(u => u.Role).Where(u => (username == null || u.Username.ToLower().StartsWith(username.ToLower())) && (name == null || u.Name.ToLower().StartsWith(name.ToLower())));

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
                    users = users.OrderByDescending(e => EF.Property<object>(e, order));
                }
                else
                {
                    users = users.OrderBy(e => EF.Property<object>(e, order));
                }
            }

            return await PaginatedList<User>.CreateAsync(users, pageNumber ?? 1, pageSize ?? 0);
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await this.FindByUsername(username);
            if (user != null && user.Password.Equals(Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: user.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8))))
            {
                return user;
            }
            return null;
        }

        public async Task<User> FindAsync(int id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> FindByUsername(string username)
        {
            return await _context.Users.Include(u => u.Role).Where(u => u.Username.Equals(username)).FirstOrDefaultAsync();
        }

        public EntityState Update(User user)
        {
            return _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(User user)
        {
            return await _context.Users.AddAsync(user);
        }

        public EntityEntry Delete(User user)
        {
            return _context.Users.Remove(user);
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
