using System;
using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMedicamentos.Controllers
{
    public class ChangeUserProfile
    {
        public string username { get; set; }
        public string name { get; set; }
    }


    [Route("api/[controller]")]
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
            user = _authService.encryptPassword(user, user.Password);
            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeProfile([FromRoute] int id, [FromBody] ChangeUserProfile userData)
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

    }

}