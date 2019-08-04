using System;
using System.Collections.Generic;

namespace GestionDeMedicamentos.Models
{
    public class StockOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public ICollection<MedicineStockOrder> MedicineStockOrders { get; set; }
    }
}
