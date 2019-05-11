using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class MedicineStockOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Medicine Medicine { get; set; }
        public StockOrder StockOrder { get; set; }
    }
}
