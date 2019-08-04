using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeMedicamentos.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Drug Drug { get; set; }
        public decimal Proportion { get; set; }
        public string Laboratory { get; set; }
        public enum PresentationTypes { Inyectable, Jarabe, Píldora, Comprimido }
        public PresentationTypes Presentation { get; set; }
        public uint Stock { get; set; }
        public int DrugId { get; set; }
        [ForeignKey("DrugId")]
        public ICollection<MedicinePrescription> MedicinePrescriptions {get; set; }
        public ICollection<MedicinePurchaseOrder> MedicinePurchaseOrders { get; set; }
        public ICollection<MedicineStockOrder> MedicineStockOrders { get; set; }
    }
}
