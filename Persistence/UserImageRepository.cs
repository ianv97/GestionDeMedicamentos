using GestionDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Persistence
{
    public interface IUserImageRepository
    {
        Task<UserImage> FindAsync(int id);

        Task<UserImage> FindByUserUsername(string username);

        Task<UserImage> FindByUserId(int id);

        Task<EntityEntry> CreateAsync(UserImage userImage);

        EntityState Update(UserImage userImage);

        EntityEntry Delete(UserImage userImage);

        Task SaveChangesAsync();

        bool UserImageExists(int id);
    }


    public class UserImageRepository : BaseRepository, IUserImageRepository
    {
        public UserImageRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<UserImage> FindAsync(int id)
        {
            return await _context.UserImages.FindAsync();
        }

        public async Task<UserImage> FindByUserId(int id)
        {
            return await _context.UserImages.Where(ui => ui.User.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserImage> FindByUserUsername(string username)
        {
            return await _context.UserImages.Where(ui => ui.User.Username.Equals(username)).FirstOrDefaultAsync();
        }

        public EntityState Update(UserImage userImage)
        {
            return _context.Entry(userImage).State = EntityState.Modified;
        }

        public async Task<EntityEntry> CreateAsync(UserImage userImage)
        {
            return await _context.UserImages.AddAsync(userImage);
        }

        public EntityEntry Delete(UserImage userImage)
        {
            return _context.UserImages.Remove(userImage);
        }

        public bool UserImageExists(int id)
        {
            return _context.UserImages.Any(ui => ui.Id == id);
        }
    }
}
