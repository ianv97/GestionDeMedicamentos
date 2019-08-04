using GestionDeMedicamentos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMedicamentos.Persistence
{
    public class PostgreContext : DbContext
    {
        public DbSet<User> Users { get; set; }
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }

    }
}
