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
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicinesController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<IEnumerable<Medicine>> GetMedicines()
        {
            return await _medicineRepository.ListAsync();
            //Enum.GetName(m.Presentation.GetType(), m.Presentation)
        }

        // GET: api/Medicines/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMedicine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicine = await _medicineRepository.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(medicine);
        }

        // GET: api/Medicines/Ibupirac
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMedicineByName([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Medicine> medicine = await _medicineRepository.FindAsyncByName(name);

            return Ok(medicine);
        }

        // PUT: api/Medicines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine([FromRoute] int id, [FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicine.Id)
            {
                return BadRequest();
            }

            _medicineRepository.Update(medicine);

            try
            {
                await _medicineRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_medicineRepository.MedicineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(medicine);
        }

        // POST: api/Medicines
        [HttpPost]
        public async Task<IActionResult> PostMedicine([FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _medicineRepository.CreateAsync(medicine);
            await _medicineRepository.SaveChangesAsync();

            return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicine = await _medicineRepository.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _medicineRepository.Delete(medicine);
            await _medicineRepository.SaveChangesAsync();

            return Ok(medicine);
        }


    }
}