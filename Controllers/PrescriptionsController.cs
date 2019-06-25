using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiónDeMedicamentos.Models;
using GestiónDeMedicamentos.Domain;

namespace GestiónDeMedicamentos.Controllers
{
    [Route("api/partidas")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMedicineRepository _medicineRepository;

        public PrescriptionsController(IPrescriptionRepository prescriptionRepository, IMedicineRepository medicineRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _medicineRepository = medicineRepository;
        }

        // GET: api/partidas
        [HttpGet]
        public async Task<IActionResult> GetPrescriptions(string date, string order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Prescription> prescriptions = await _prescriptionRepository.ListAsync(date, order);

            return Ok(prescriptions);
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
        public async Task<IActionResult> PutPrescription([FromRoute] int id, [FromBody] Prescription prescriptionUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescriptionUpdated.Id)
            {
                return BadRequest();
            }

            var prescription = await _prescriptionRepository.FindAsync(id);
            foreach (var medicinePrescription in prescription.MedicinePrescriptions)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePrescription.MedicineId);
                medicine.Stock += medicinePrescription.Quantity;
                _medicineRepository.Update(medicine);
            }
            foreach (var medicinePrescriptionUpdated in prescriptionUpdated.MedicinePrescriptions)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePrescriptionUpdated.MedicineId);
                medicine.Stock -= medicinePrescriptionUpdated.Quantity;
                _medicineRepository.Update(medicine);
            }

            _prescriptionRepository.Update(prescriptionUpdated);

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

            foreach (var medicinePrescription in prescription.MedicinePrescriptions)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePrescription.MedicineId);
                medicine.Stock -= medicinePrescription.Quantity;
                _medicineRepository.Update(medicine);
            }

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

            foreach (var medicinePrescription in prescription.MedicinePrescriptions)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePrescription.MedicineId);
                medicine.Stock += medicinePrescription.Quantity;
                _medicineRepository.Update(medicine);
            }

            _prescriptionRepository.Delete(prescription);

            await _prescriptionRepository.SaveChangesAsync();

            return Ok(prescription);
        }
    }
}