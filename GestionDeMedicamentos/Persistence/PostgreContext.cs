using GestiónDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestiónDeMedicamentos.Database
{
    public class PostgreContext : DbContext
    {
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicinePrescription> MedicinePrescriptions { get; set; }
        public DbSet<MedicinePurchaseOrder> MedicinePurchaseOrders { get; set; }
        public DbSet<MedicineStockOrder> MedicineStockOrders { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<StockOrder> StockOrders { get; set; }

        public PostgreContext(DbContextOptions<PostgreContext> options) : base(options)
        {

        }

    }
}
