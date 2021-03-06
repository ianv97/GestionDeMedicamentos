﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeMedicamentos.Models
{
    public class MedicinePrescription
    {
        public int Id { get; set; }
        public uint Quantity { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        [Required]
        public int PrescriptionId { get; set; }
        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; }
    }
}
