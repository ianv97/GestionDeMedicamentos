using System;
using System.Collections.Generic;

namespace GestionDeMedicamentos.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MedicinePurchaseOrder> MedicinePurchaseOrders { get; set; }
    }
}
