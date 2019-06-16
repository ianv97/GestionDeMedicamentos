using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestiónDeMedicamentos.Migrations
{
    public partial class MedicinePurchaseOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescriptions_Medicines_MedicineId",
                table: "MedicinePrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescriptions_Prescriptions_PrescriptionId",
                table: "MedicinePrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchaseOrders_Medicines_MedicineId",
                table: "MedicinePurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchaseOrders_PurchaseOrders_PurchaseOrderId",
                table: "MedicinePurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrders_Medicines_MedicineId",
                table: "MedicineStockOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrders_StockOrders_StockOrderId",
                table: "MedicineStockOrders");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "MedicinePurchaseOrders");

            migrationBuilder.AlterColumn<int>(
                name: "StockOrderId",
                table: "MedicineStockOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicineStockOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderId",
                table: "MedicinePurchaseOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicinePurchaseOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "MedicinePrescriptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicinePrescriptions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrugId",
                table: "MedicinePrescriptions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePrescriptions_DrugId",
                table: "MedicinePrescriptions",
                column: "DrugId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescriptions_Medicines_DrugId",
                table: "MedicinePrescriptions",
                column: "DrugId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescriptions_Medicines_MedicineId",
                table: "MedicinePrescriptions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescriptions_Prescriptions_PrescriptionId",
                table: "MedicinePrescriptions",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchaseOrders_Medicines_MedicineId",
                table: "MedicinePurchaseOrders",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchaseOrders_PurchaseOrders_PurchaseOrderId",
                table: "MedicinePurchaseOrders",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineStockOrders_Medicines_MedicineId",
                table: "MedicineStockOrders",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineStockOrders_StockOrders_StockOrderId",
                table: "MedicineStockOrders",
                column: "StockOrderId",
                principalTable: "StockOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescriptions_Medicines_DrugId",
                table: "MedicinePrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescriptions_Medicines_MedicineId",
                table: "MedicinePrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescriptions_Prescriptions_PrescriptionId",
                table: "MedicinePrescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchaseOrders_Medicines_MedicineId",
                table: "MedicinePurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchaseOrders_PurchaseOrders_PurchaseOrderId",
                table: "MedicinePurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrders_Medicines_MedicineId",
                table: "MedicineStockOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrders_StockOrders_StockOrderId",
                table: "MedicineStockOrders");

            migrationBuilder.DropIndex(
                name: "IX_MedicinePrescriptions_DrugId",
                table: "MedicinePrescriptions");

            migrationBuilder.DropColumn(
                name: "DrugId",
                table: "MedicinePrescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "StockOrderId",
                table: "MedicineStockOrders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicineStockOrders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderId",
                table: "MedicinePurchaseOrders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicinePurchaseOrders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "MedicinePurchaseOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "PrescriptionId",
                table: "MedicinePrescriptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MedicineId",
                table: "MedicinePrescriptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescriptions_Medicines_MedicineId",
                table: "MedicinePrescriptions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescriptions_Prescriptions_PrescriptionId",
                table: "MedicinePrescriptions",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchaseOrders_Medicines_MedicineId",
                table: "MedicinePurchaseOrders",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchaseOrders_PurchaseOrders_PurchaseOrderId",
                table: "MedicinePurchaseOrders",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineStockOrders_Medicines_MedicineId",
                table: "MedicineStockOrders",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineStockOrders_StockOrders_StockOrderId",
                table: "MedicineStockOrders",
                column: "StockOrderId",
                principalTable: "StockOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
