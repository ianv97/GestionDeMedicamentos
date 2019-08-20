using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.AspNetCore.Mvc;
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
    public class ChangeUserProfile
    {
        public string username { get; set; }
        public string name { get; set; }
    }

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [Route("api/[controller]/{id}")]
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
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            user = _authService.encryptPassword(user, user.Password);
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> ChangeProfileData([FromRoute] int id, [FromBody] ChangeUserProfile userData)
        {
            User user = await _userRepository.FindAsync(id);
            if (user != null)
            {
                user.Name = userData.name;
                user.Username = userData.username;
                _userRepository.Update(user);
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
                return NotFound();
            }
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPut]
        [Route("api/change-password/{id}")]
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