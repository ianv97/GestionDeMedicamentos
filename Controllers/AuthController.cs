using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeMedicamentos.Controllers
{
    public class UserData
    {
        public string username { get; set; }
        public string password { get; set; }
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
        public IActionResult Login([FromBody] UserData data)
        {
            User user = _userRepository.Login(data.username, data.password);
            if ( user != null)
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
    }
}