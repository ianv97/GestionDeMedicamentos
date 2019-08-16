using System;
using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMedicamentos.Controllers
{
    public class ChangeUserPassword
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string newPasswordV { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            user = encryptPassword(user, user.Password);
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangePassword([FromRoute] int id, [FromBody] ChangeUserPassword userData)
        {
            if (userData.newPassword == userData.newPasswordV)
            {
                User user = await _userRepository.Login(userData.username, userData.oldPassword);
                if (user != null)
                {
                    if (id != user.Id)
                    {
                        return BadRequest();
                    }
                    _userRepository.Update(this.encryptPassword(user, userData.newPassword));
                    try
                    {
                        await _userRepository.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_userRepository.UserExists(user.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
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