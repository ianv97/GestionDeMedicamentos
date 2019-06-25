using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                if (medicinePrescription.Quantity <= medicine.Stock)
                {
                    medicine.Stock -= medicinePrescription.Quantity;
                    _medicineRepository.Update(medicine);
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
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