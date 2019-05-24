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
    [Route("api/partidas")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionsController(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        // GET: api/partidas
        [HttpGet]
        public async Task<IEnumerable<Prescription>> GetPrescriptions(DateTime date, string order)
        {
            return await _prescriptionRepository.ListAsync(date, order);
        }

        // GET: api/partidas/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPrescription([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prescription = await _prescriptionRepository.FindAsync(id);

            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(prescription);
        }


        // PUT: api/partidas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrescription([FromRoute] int id, [FromBody] Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescription.Id)
            {
                return BadRequest();
            }

            _prescriptionRepository.Update(prescription);

            try
            {
                await _prescriptionRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_prescriptionRepository.PrescriptionExists(id))
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

        // POST: api/partidas
        [HttpPost]
        public async Task<IActionResult> PostPrescription([FromBody] Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _prescriptionRepository.CreateAsync(prescription);
            await _prescriptionRepository.SaveChangesAsync();

            return CreatedAtAction("GetPrescription", new { id = prescription.Id }, prescription);
        }

        // DELETE: api/partidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrescription([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prescription = await _prescriptionRepository.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            _prescriptionRepository.Delete(prescription);
            await _prescriptionRepository.SaveChangesAsync();

            return Ok(prescription);
        }
    }
}