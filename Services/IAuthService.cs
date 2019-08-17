using System;
using GestionDeMedicamentos.Models;

namespace GestionDeMedicamentos.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username, string password, TimeSpan validDate);
        User encryptPassword(User user, string password);
    }
}
