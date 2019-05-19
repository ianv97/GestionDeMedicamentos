using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Models;
using GestiónDeMedicamentos.Domain;

namespace GestiónDeMedicamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository _drugRepository;

        public DrugsController(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }

        // GET: api/Drugs
        [HttpGet]
        public async Task<IEnumerable<Drug>> GetDrugs()
        {
            return await _drugRepository.ListAsync();
        }

        // GET: api/Drugs/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDrug([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drug = await _drugRepository.FindAsync(id);

            if (drug == null)
            {
                return NotFound();
            }

            return Ok(drug);
        }

        // GET: api/Drugs/Ibuprofeno
        [HttpGet("{name}")]
        public async Task<IActionResult> GetDrugByName([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Drug> drugs = await _drugRepository.FindAsyncByName(name);

            return Ok(drugs);
        }

        // PUT: api/Drugs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrug([FromRoute] int id, [FromBody] Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drug.Id)
            {
                return BadRequest();
            }

            _drugRepository.Update(drug);

            try
            {
                await _drugRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_drugRepository.DrugExists(id))
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

        // POST: api/Drugs
        [HttpPost]
        public async Task<IActionResult> PostDrug([FromBody] Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _drugRepository.CreateAsync(drug);
            await _drugRepository.SaveChangesAsync();

            return CreatedAtAction("GetDrug", new { id = drug.Id }, drug);
        }

        // DELETE: api/Drugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrug([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var drug = await _drugRepository.FindAsync(id);
            if (drug == null)
            {
                return NotFound();
            }

            _drugRepository.Delete(drug);
            await _drugRepository.SaveChangesAsync();

            return Ok(drug);
        }


    }
}