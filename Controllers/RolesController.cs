using GestionDeMedicamentos.Persistence;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GestionDeMedicamentos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository, IAuthService authService)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles(string name, string order, int? pageNumber, int? pageSize)
        {
            PaginatedList<Role> roles = await _roleRepository.ListAsync(name, order, pageNumber, pageSize);
            HttpContext.Response.Headers.Add("page", roles.PageIndex.ToString());
            HttpContext.Response.Headers.Add("totalRecords", roles.TotalRecords.ToString());
            return Ok(roles);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var role = await _roleRepository.FindAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            await _roleRepository.CreateAsync(role);
            await _roleRepository.SaveChangesAsync();
            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditRole([FromRoute] int id, [FromBody] Role role)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != role.Id) return BadRequest();

            _roleRepository.Update(role);

            try
            {
                await _roleRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_roleRepository.RoleExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var role = await _roleRepository.FindAsync(id);
            if (role == null) return NotFound();
            _roleRepository.Delete(role);
            await _roleRepository.SaveChangesAsync();
            return NoContent();
        }

    }

}