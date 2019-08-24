using GestionDeMedicamentos.Persistence;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
    public class UserData
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UsersController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [Authorize(Roles = "Administrador")]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetUsers(string username, string name, string order, int? pageNumber, int? pageSize)
        {
            PaginatedList<User> users = await _userRepository.ListAsync(username, name, order, pageNumber, pageSize);
            HttpContext.Response.Headers.Add("page", users.PageIndex.ToString());
            HttpContext.Response.Headers.Add("totalRecords", users.TotalRecords.ToString());
            return Ok(users);
        }

        [Authorize]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (id.ToString() != User.Identity.Name) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userRepository.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Route("api/auth")]
        public async Task<IActionResult> Login([FromBody] UserData data)
        {
            User user = await _userRepository.Login(data.username, data.password);
            if (user == null) return BadRequest();
            user.Password = null;
            System.TimeSpan expire = System.TimeSpan.FromDays(3);
            string token = _authService.GenerateToken(user, expire);
            return Ok(new
            {
                token,
                expireAt = System.DateTime.UtcNow.Add(expire),
                user
            });

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

        [Authorize]
        [HttpPut]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> ChangeProfileData([FromRoute] int id, [FromBody] ChangeUserProfile userData)
        {
            if (id.ToString() != User.Identity.Name) return Unauthorized();
            User user = await _userRepository.FindAsync(id);
            if (user == null) return NotFound();
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

        [Authorize]
        [HttpPut]
        [Route("api/change-password/{id}")]
        public async Task<IActionResult> ChangePassword([FromRoute] int id, [FromBody] ChangeUserPassword userData)
        {
            if (id.ToString() != User.Identity.Name) return Unauthorized();
            if (userData.newPassword != userData.newPasswordV) return BadRequest();
            User user = await _userRepository.Login(userData.username, userData.oldPassword);
            if (user == null) return Unauthorized();
            if (id != user.Id) return BadRequest();
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

    }

}