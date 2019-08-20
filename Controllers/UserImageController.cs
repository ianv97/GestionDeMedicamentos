using GestionDeMedicamentos.Domain;
using GestionDeMedicamentos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GestionDeMedicamentos.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserImageRepository _userImageRepository;

        public UserImageController(IUserRepository userRepository, IUserImageRepository userImageRepository)
        {
            _userRepository = userRepository;
            _userImageRepository = userImageRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserImage([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userImage = await _userImageRepository.FindByUserId(id);

            if (userImage == null)
            {
                return NotFound();
            }

            return Ok(userImage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeAvatar(IFormFile image, [FromRoute] int id)
        {
            if (_userRepository.UserExists(id))
            {
                bool newImage = false;
                UserImage userImage = await _userImageRepository.FindByUserId(id);
                if (userImage == null)
                {
                    userImage = new UserImage();
                    userImage.UserId = id;
                    newImage = true;
                }
                using (BinaryReader binaryReader = new BinaryReader(image.OpenReadStream()))
                {
                    userImage.Img = binaryReader.ReadBytes((int)image.Length);
                }
                try
                {
                    if (newImage)
                    {
                        await _userImageRepository.CreateAsync(userImage);
                    }
                    else
                    {
                        _userImageRepository.Update(userImage);
                    }
                    await _userRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_userRepository.UserExists(userImage.Id))
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