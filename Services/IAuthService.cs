using System;

namespace GestionDeMedicamentos.Services
{
    public interface IAuthService
    {
        string GenerateToken(string username, string password, TimeSpan validDate);
    }
}
