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
    [Route("api/reposiciones")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IMedicineRepository _medicineRepository;

        public PurchaseOrdersController(IPurchaseOrderRepository purchaseOrderRepository, IMedicineRepository medicineRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _medicineRepository = medicineRepository;
        }

        //GET: api/reposiciones?date=01/01/2019
        [HttpGet]
        public async Task<IActionResult> GetPurchaseOrders(DateTime? date, string order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<PurchaseOrder> purchaseOrders = await _purchaseOrderRepository.ListAsync(date, order);

            return Ok(purchaseOrders);
        }

        // GET: api/reposiciones/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPurchaseOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrder = await _purchaseOrderRepository.FindAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrder);
        }

        // PUT: api/reposiciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrder([FromRoute] int id, [FromBody] PurchaseOrder purchaseOrderUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrderUpdated.Id)
            {
                return BadRequest();
            }

            var purchaseOrder = await _purchaseOrderRepository.FindAsync(id);
            foreach (var medicinePurchaseOrder in purchaseOrder.MedicinePurchaseOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePurchaseOrder.MedicineId);
                medicine.Stock -= medicinePurchaseOrder.Quantity;
                _medicineRepository.Update(medicine);
            }
            foreach (var medicinePurchaseOrderUpdated in purchaseOrderUpdated.MedicinePurchaseOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePurchaseOrderUpdated.MedicineId);
                medicine.Stock += medicinePurchaseOrderUpdated.Quantity;
                _medicineRepository.Update(medicine);
            }

            _purchaseOrderRepository.Update(purchaseOrderUpdated);

            try
            {
                await _purchaseOrderRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_purchaseOrderRepository.PurchaseOrderExists(id))
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

        // POST: api/reposiciones
        [HttpPost]
        public async Task<IActionResult> PostPurchaseOrder([FromBody] PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _purchaseOrderRepository.CreateAsync(purchaseOrder);

            foreach (var medicinePurchaseOrder in purchaseOrder.MedicinePurchaseOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePurchaseOrder.MedicineId);
                medicine.Stock += medicinePurchaseOrder.Quantity;
                _medicineRepository.Update(medicine);
            }

            await _purchaseOrderRepository.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseOrders", new { id = purchaseOrder.Id }, purchaseOrder);
        }

        // DELETE: api/reposiciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrder = await _purchaseOrderRepository.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            foreach (var medicinePurchaseOrder in purchaseOrder.MedicinePurchaseOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicinePurchaseOrder.MedicineId);
                medicine.Stock -= medicinePurchaseOrder.Quantity;
                _medicineRepository.Update(medicine);
            }
            _purchaseOrderRepository.Delete(purchaseOrder);

            await _purchaseOrderRepository.SaveChangesAsync();

            return Ok(purchaseOrder);
        }


    }
}