using GestionDeMedicamentos.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using GestionDeMedicamentos.Models;

namespace GestionDeMedicamentos.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration Configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(string username, string password, TimeSpan validDate)
        {
            DateTime date = DateTime.UtcNow;
            var expire = date.Add(validDate);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Aud, Configuration["AuthSettings:Audience"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("roles", "Administrador"),
                new Claim("roles", "Usuario")
            };
            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration["AuthSettings:SigninKey"])), SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                issuer: Configuration["AuthSettings:Issuer"],
                claims: claims,
                notBefore: date,
                expires: expire,
                signingCredentials: signinCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public User encryptPassword(User user, string password)
        {
            //Crea la salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            user.Salt = salt;

            //Encripta la contraseña y la salt guardándolas en el campo Password
            user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

            return user;
        }
    }
}
