using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestiónDeMedicamentos.Models;
using GestiónDeMedicamentos.Domain;
using GestionDeMedicamentos.Controllers;

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
        public async Task<IActionResult> GetPurchaseOrders(string date, string order, int? pageNumber, int? pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PaginatedList<PurchaseOrder> purchaseOrders = await _purchaseOrderRepository.ListAsync(date, order, pageNumber, pageSize);
            HttpContext.Response.Headers.Add("page", purchaseOrders.PageIndex.ToString());
            HttpContext.Response.Headers.Add("totalRecords", purchaseOrders.TotalRecords.ToString());
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