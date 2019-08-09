using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionDeMedicamentos.Models;
using GestionDeMedicamentos.Domain;
using System;
using GestionDeMedicamentos.Services;

namespace GestionDeMedicamentos.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
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
        public async Task<IActionResult> GetStockOrders(string date, string order, int? pageNumber, int? pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaginatedList<StockOrder> stockOrders = await _stockOrderRepository.ListAsync(date, order, pageNumber, pageSize);
            HttpContext.Response.Headers.Add("page", stockOrders.PageIndex.ToString());
            HttpContext.Response.Headers.Add("totalRecords", stockOrders.TotalRecords.ToString());
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
                if (medicineStockOrder.Quantity < 0)
                {
                    medicine.Stock -= (uint)Math.Abs(medicineStockOrder.Quantity);
                }
                else
                {
                    medicine.Stock += (uint)medicineStockOrder.Quantity;
                }
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
                if (medicineStockOrder.Quantity < 0)
                {
                    medicine.Stock += (uint)Math.Abs(medicineStockOrder.Quantity);
                }
                else
                {
                    medicine.Stock -= (uint)medicineStockOrder.Quantity;
                }
                _medicineRepository.Update(medicine);
            }

            _stockOrderRepository.Delete(stockOrder);
            await _stockOrderRepository.SaveChangesAsync();

            return Ok(stockOrder);
        }


    }
}