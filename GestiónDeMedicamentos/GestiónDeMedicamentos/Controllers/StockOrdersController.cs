using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestiónDeMedicamentos.Database;
using GestiónDeMedicamentos.Models;

namespace GestiónDeMedicamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockOrdersController : ControllerBase
    {
        private readonly PostgreContext _context;

        public StockOrdersController(PostgreContext context)
        {
            _context = context;
        }

        // GET: api/StockOrders
        [HttpGet]
        public IEnumerable<StockOrder> GetStockOrders()
        {
            return _context.StockOrders;
        }

        // GET: api/StockOrders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockOrder = await _context.StockOrders.FindAsync(id);

            if (stockOrder == null)
            {
                return NotFound();
            }

            return Ok(stockOrder);
        }

        // PUT: api/StockOrders/5
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

            _context.Entry(stockOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockOrderExists(id))
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

        // POST: api/StockOrders
        [HttpPost]
        public async Task<IActionResult> PostStockOrder([FromBody] StockOrder stockOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.StockOrders.Add(stockOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockOrder", new { id = stockOrder.Id }, stockOrder);
        }

        // DELETE: api/StockOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockOrder = await _context.StockOrders.FindAsync(id);
            if (stockOrder == null)
            {
                return NotFound();
            }

            _context.StockOrders.Remove(stockOrder);
            await _context.SaveChangesAsync();

            return Ok(stockOrder);
        }

        private bool StockOrderExists(int id)
        {
            return _context.StockOrders.Any(e => e.Id == id);
        }
    }
}