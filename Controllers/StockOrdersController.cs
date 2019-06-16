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
    [Route("api/stock")]
    [ApiController]
    public class StockOrdersController : ControllerBase
    {
        private readonly IStockOrderRepository _stockOrderRepository;
        private readonly IMedicineRepository _medicineRepository;

        public StockOrdersController(IStockOrderRepository stockOrderRepository, IMedicineRepository medicineRepository)
        {
            _stockOrderRepository = stockOrderRepository;
            _medicineRepository = medicineRepository;
        }

        //GET: api/stock/?date=01/01/2019
        [HttpGet]
        public async Task<IActionResult> GetStockOrders(DateTime? date, string order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<StockOrder> stockOrders = await _stockOrderRepository.ListAsync(date, order);

            return Ok(stockOrders);
        }


        // GET: api/stock/5
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


        // PUT: api/stock/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockOrder([FromRoute] int id, [FromBody] StockOrder stockOrderUpdated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockOrderUpdated.Id)
            {
                return BadRequest();
            }

            var stockOrder = await _stockOrderRepository.FindAsync(id);
            foreach (var medicineStockOrder in stockOrder.MedicineStockOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicineStockOrder.MedicineId);
                medicine.Stock -= medicineStockOrder.Quantity;
                _medicineRepository.Update(medicine);
            }
            foreach (var medicineStockOrderUpdated in stockOrderUpdated.MedicineStockOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicineStockOrderUpdated.MedicineId);
                medicine.Stock += medicineStockOrderUpdated.Quantity;
                _medicineRepository.Update(medicine);
            }

            _stockOrderRepository.Update(stockOrderUpdated);

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

            foreach (var medicineStockOrder in stockOrder.MedicineStockOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicineStockOrder.MedicineId);
                medicine.Stock += medicineStockOrder.Quantity;
                _medicineRepository.Update(medicine);
            }

            await _stockOrderRepository.SaveChangesAsync();

            return CreatedAtAction("GetStockOrder", new { id = stockOrder.Id }, stockOrder);
        }

        // DELETE: api/stock/5
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

            foreach (var medicineStockOrder in stockOrder.MedicineStockOrders)
            {
                Medicine medicine = await _medicineRepository.FindAsync(medicineStockOrder.MedicineId);
                medicine.Stock -= medicineStockOrder.Quantity;
                _medicineRepository.Update(medicine);
            }

            _stockOrderRepository.Delete(stockOrder);
            await _stockOrderRepository.SaveChangesAsync();

            return Ok(stockOrder);
        }


    }
}