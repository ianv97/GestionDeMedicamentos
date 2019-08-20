using GestionDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GestionDeMedicamentos.Domain
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
}
