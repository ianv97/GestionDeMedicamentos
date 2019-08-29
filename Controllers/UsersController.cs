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
    public class UserLoginData
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
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetUsers(string name, string username, string role, string order, int? pageNumber, int? pageSize)
        {
            PaginatedList<User> users = await _userRepository.ListAsync(name, username, role, order, pageNumber, pageSize);
            HttpContext.Response.Headers.Add("page", users.PageIndex.ToString());
            HttpContext.Response.Headers.Add("totalRecords", users.TotalRecords.ToString());
            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (id.ToString() != User.Identity.Name && !User.IsInRole("Administrador")) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userRepository.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Route("api/auth")]
        public async Task<IActionResult> Login([FromBody] UserLoginData data)
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
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] User userData)
        {
            if (id.ToString() != User.Identity.Name && !User.IsInRole("Administrador")) return Unauthorized();
            User user = await _userRepository.FindAsync(id);
            if (user == null) return NotFound();
            user.Name = userData.Name;
            user.Username = userData.Username;
            if (User.IsInRole("Administrador")) user.RoleId = userData.RoleId;
            if (User.IsInRole("Administrador") && userData.Password != null)
            {
                _userRepository.Update(_authService.encryptPassword(user, userData.Password));
            }
            else
            {
                _userRepository.Update(user);
            }
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

        [Authorize(Roles = "Administrador")]
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userRepository.FindAsync(id);
            if (user == null) return NotFound();
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return NoContent();
        }

    }

}