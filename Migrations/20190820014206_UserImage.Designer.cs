﻿// <auto-generated />
using System;
using GestionDeMedicamentos.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GestiónDeMedicamentos.Migrations
{
    [DbContext(typeof(PostgreContext))]
    [Migration("20190820014206_UserImage")]
    partial class UserImage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("GestionDeMedicamentos.Models.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DrugId");

                    b.Property<string>("Laboratory");

                    b.Property<string>("Name");

                    b.Property<int>("Presentation");

                    b.Property<decimal>("Proportion");

                    b.Property<long>("Stock");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.MedicinePrescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DrugId");

                    b.Property<int>("MedicineId");

                    b.Property<int>("PrescriptionId");

                    b.Property<long>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PrescriptionId");

                    b.ToTable("MedicinePrescriptions");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.MedicinePurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MedicineId");

                    b.Property<int>("PurchaseOrderId");

                    b.Property<long>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("MedicinePurchaseOrders");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.MedicineStockOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MedicineId");

                    b.Property<int>("Quantity");

                    b.Property<int>("StockOrderId");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("StockOrderId");

                    b.ToTable("MedicineStockOrders");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.StockOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.HasKey("Id");

                    b.ToTable("StockOrders");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.Property<byte[]>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.UserImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Img");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.Medicine", b =>
                {
                    b.HasOne("GestionDeMedicamentos.Models.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.MedicinePrescription", b =>
                {
                    b.HasOne("GestionDeMedicamentos.Models.Medicine")
                        .WithMany("MedicinePrescriptions")
                        .HasForeignKey("DrugId");

                    b.HasOne("GestionDeMedicamentos.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionDeMedicamentos.Models.Prescription", "Prescription")
                        .WithMany("MedicinePrescriptions")
                        .HasForeignKey("PrescriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.MedicinePurchaseOrder", b =>
                {
                    b.HasOne("GestionDeMedicamentos.Models.Medicine", "Medicine")
                        .WithMany("MedicinePurchaseOrders")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionDeMedicamentos.Models.PurchaseOrder", "PurchaseOrder")
                        .WithMany("MedicinePurchaseOrders")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.MedicineStockOrder", b =>
                {
                    b.HasOne("GestionDeMedicamentos.Models.Medicine", "Medicine")
                        .WithMany("MedicineStockOrders")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestionDeMedicamentos.Models.StockOrder", "StockOrder")
                        .WithMany("MedicineStockOrders")
                        .HasForeignKey("StockOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.User", b =>
                {
                    b.HasOne("GestionDeMedicamentos.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestionDeMedicamentos.Models.UserImage", b =>
                {
                    b.HasOne("GestionDeMedicamentos.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
