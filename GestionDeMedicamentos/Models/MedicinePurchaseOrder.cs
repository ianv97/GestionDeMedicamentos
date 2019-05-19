using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class MedicinePurchaseOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Medicine Medicine { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
    }
}
