using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Models
{
    public class MedicinePrescription
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Medicine Medicine { get; set; }
        public Prescription Prescription { get; set; }
    }
}
