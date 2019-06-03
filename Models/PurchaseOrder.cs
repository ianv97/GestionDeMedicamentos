using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MedicinePurchaseOrder> MedicinePurchaseOrders { get; set; }
    }
}
