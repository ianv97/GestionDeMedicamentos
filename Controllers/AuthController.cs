using System.Threading.Tasks;
using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMedicamentos.Controllers
{
    public class UserData
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class ChangeUserPassword
    {
        public string username { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string newPasswordV { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserData data)
        {
            User user = await _userRepository.Login(data.username, data.password);
            if (user != null)
            {
                System.TimeSpan expire = System.TimeSpan.FromHours(3);
                string token = _authService.GenerateToken(data.username, data.password, expire);
                return Ok(new
                {
                    token,
                    expireAt = System.DateTime.UtcNow.Add(expire),
                    user
                });
            }
            else
            {
                return StatusCode(401);
            }

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
                    _userRepository.Update(_authService.encryptPassword(user, userData.newPassword));
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
    }
}