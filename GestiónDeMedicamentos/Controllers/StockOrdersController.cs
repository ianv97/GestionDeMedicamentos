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
    public class StockOrdersController : ControllerBase
    {
        private readonly IStockOrderRepository _stockOrderRepository;

        public StockOrdersController(IStockOrderRepository stockOrderRepository)
        {
            _stockOrderRepository = stockOrderRepository;
        }

        // GET: api/StockOrder
        [HttpGet]
        public async Task<IEnumerable<StockOrder>> GetStockOrders()
        {
            return await _stockOrderRepository.ListAsync();
        }

        // GET: api/StockOrder/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockOrder = await _stockOrderRepository.FindAsync(id);

            if (stockOrder == null)
            {
                return NotFound();
            }

            return Ok(stockOrder);
        }


        // PUT: api/StockOrder/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockOrder([FromRoute] int id, [FromBody] StockOrder stockOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockOrder.Id)
            {
                return BadRequest();
            }

            _stockOrderRepository.Update(stockOrder);

            try
            {
                await _stockOrderRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_stockOrderRepository.StockOrderExists(id))
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

        // POST: api/StockOrder
        [HttpPost]
        public async Task<IActionResult> PostStockOrder([FromBody] StockOrder stockOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _stockOrderRepository.CreateAsync(stockOrder);
            await _stockOrderRepository.SaveChangesAsync();

            return CreatedAtAction("GetStockOrder", new { id = stockOrder.Id }, stockOrder);
        }

        // DELETE: api/StockOrder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockOrder = await _stockOrderRepository.FindAsync(id);
            if (stockOrder == null)
            {
                return NotFound();
            }

            _stockOrderRepository.Delete(stockOrder);
            await _stockOrderRepository.SaveChangesAsync();

            return Ok(stockOrder);
        }


    }
}