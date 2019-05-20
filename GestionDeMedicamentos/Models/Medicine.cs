using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public enum PresentationTypes { Inyectable, Jarabe, Píldora, Comprimido }
        public PresentationTypes Presentation { get; set; }
        public int Stock { get; set; }
        public int? DrugId { get; set; }
        [ForeignKey("DrugId")]
        public Drug Drug { get; set; }
        public ICollection<MedicinePrescription> MedicinePrescriptions {get; set; }
        public ICollection<MedicinePurchaseOrder> MedicinePurchaseOrders { get; set; }
        public ICollection<MedicineStockOrder> MedicineStockOrders { get; set; }
    }
}
