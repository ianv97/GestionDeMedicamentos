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
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrdersController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        // GET: api/PurchaseOrders
        [HttpGet]
        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrders()
        {
            return await _purchaseOrderRepository.ListAsync();
        }

        // GET: api/PurchaseOrders/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPurchaseOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrders = await _purchaseOrderRepository.FindAsync(id);

            if (purchaseOrders == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrders);
        }


        // PUT: api/PurchaseOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrders([FromRoute] int id, [FromBody] PurchaseOrder purchaseOrders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrders.Id)
            {
                return BadRequest();
            }

            _purchaseOrderRepository.Update(purchaseOrders);

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

        // POST: api/PurchaseOrders
        [HttpPost]
        public async Task<IActionResult> PostDrug([FromBody] PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _purchaseOrderRepository.CreateAsync(purchaseOrder);
            await _purchaseOrderRepository.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseOrders", new { id = purchaseOrder.Id }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrders = await _purchaseOrderRepository.FindAsync(id);
            if (purchaseOrders == null)
            {
                return NotFound();
            }

            _purchaseOrderRepository.Delete(purchaseOrders);
            await _purchaseOrderRepository.SaveChangesAsync();

            return Ok(purchaseOrders);
        }


    }
}