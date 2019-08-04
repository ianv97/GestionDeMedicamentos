using System;
using System.Collections.Generic;

namespace GestionDeMedicamentos.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<MedicinePrescription> MedicinePrescriptions { get; set; }
    }
}
