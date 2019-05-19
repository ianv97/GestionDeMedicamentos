using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Proportion { get; set; }
        public string Laboratory { get; set; }
        public enum Presentation { Inyectable, Jarabe, Pildora, Comprimido }
        public int Stock { get; set; }
        public Drug Drug { get; set; }
        public ICollection<MedicinePrescription> MedicinePrescriptions {get; set; }
        public ICollection<MedicinePurchaseOrder> MedicinePurchaseOrders { get; set; }
        public ICollection<MedicineStockOrder> MedicineStockOrders { get; set; }
    }
}
