using GestionDeMedicamentos.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

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
    }
}
