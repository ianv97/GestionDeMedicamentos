using Microsoft.EntityFrameworkCore.Migrations;

namespace GestiónDeMedicamentos.Migrations
{
    public partial class MedicinePresentations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescription_Medicines_MedicineId",
                table: "MedicinePrescription");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescription_Prescription_PrescriptionId",
                table: "MedicinePrescription");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchaseOrder_Medicines_MedicineId",
                table: "MedicinePurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePurchaseOrder_PurchaseOrder_PurchaseOrderId",
                table: "MedicinePurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Drug_DrugId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrder_Medicines_MedicineId",
                table: "MedicineStockOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrder_StockOrder_StockOrderId",
                table: "MedicineStockOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrder",
                table: "StockOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrder",
                table: "PurchaseOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicineStockOrder",
                table: "MedicineStockOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicinePurchaseOrder",
                table: "MedicinePurchaseOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicinePrescription",
                table: "MedicinePrescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drug",
                table: "Drug");

            migrationBuilder.RenameTable(
                name: "StockOrder",
                newName: "StockOrders");

            migrationBuilder.RenameTable(
                name: "PurchaseOrder",
                newName: "PurchaseOrders");

            migrationBuilder.RenameTable(
                name: "Prescription",
                newName: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "MedicineStockOrder",
                newName: "MedicineStockOrders");

            migrationBuilder.RenameTable(
                name: "MedicinePurchaseOrder",
                newName: "MedicinePurchaseOrders");

            migrationBuilder.RenameTable(
                name: "MedicinePrescription",
                newName: "MedicinePrescriptions");

            migrationBuilder.RenameTable(
                name: "Drug",
                newName: "Drugs");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineStockOrder_StockOrderId",
                table: "MedicineStockOrders",
                newName: "IX_MedicineStockOrders_StockOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineStockOrder_MedicineId",
                table: "MedicineStockOrders",
                newName: "IX_MedicineStockOrders_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePurchaseOrder_PurchaseOrderId",
                table: "MedicinePurchaseOrders",
                newName: "IX_MedicinePurchaseOrders_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePurchaseOrder_MedicineId",
                table: "MedicinePurchaseOrders",
                newName: "IX_MedicinePurchaseOrders_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePrescription_PrescriptionId",
                table: "MedicinePrescriptions",
                newName: "IX_MedicinePrescriptions_PrescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePrescription_MedicineId",
                table: "MedicinePrescriptions",
                newName: "IX_MedicinePrescriptions_MedicineId");

            migrationBuilder.AddColumn<int>(
                name: "Presentation",
                table: "Medicines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrders",
                table: "StockOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicineStockOrders",
                table: "MedicineStockOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicinePurchaseOrders",
                table: "MedicinePurchaseOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicinePrescriptions",
                table: "MedicinePrescriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drugs",
                table: "Drugs",
                column: "Id");

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
                name: "FK_Medicines_Drugs_DrugId",
                table: "Medicines",
                column: "DrugId",
                principalTable: "Drugs",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Medicines_Drugs_DrugId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrders_Medicines_MedicineId",
                table: "MedicineStockOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineStockOrders_StockOrders_StockOrderId",
                table: "MedicineStockOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockOrders",
                table: "StockOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prescriptions",
                table: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicineStockOrders",
                table: "MedicineStockOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicinePurchaseOrders",
                table: "MedicinePurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicinePrescriptions",
                table: "MedicinePrescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drugs",
                table: "Drugs");

            migrationBuilder.DropColumn(
                name: "Presentation",
                table: "Medicines");

            migrationBuilder.RenameTable(
                name: "StockOrders",
                newName: "StockOrder");

            migrationBuilder.RenameTable(
                name: "PurchaseOrders",
                newName: "PurchaseOrder");

            migrationBuilder.RenameTable(
                name: "Prescriptions",
                newName: "Prescription");

            migrationBuilder.RenameTable(
                name: "MedicineStockOrders",
                newName: "MedicineStockOrder");

            migrationBuilder.RenameTable(
                name: "MedicinePurchaseOrders",
                newName: "MedicinePurchaseOrder");

            migrationBuilder.RenameTable(
                name: "MedicinePrescriptions",
                newName: "MedicinePrescription");

            migrationBuilder.RenameTable(
                name: "Drugs",
                newName: "Drug");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineStockOrders_StockOrderId",
                table: "MedicineStockOrder",
                newName: "IX_MedicineStockOrder_StockOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineStockOrders_MedicineId",
                table: "MedicineStockOrder",
                newName: "IX_MedicineStockOrder_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePurchaseOrders_PurchaseOrderId",
                table: "MedicinePurchaseOrder",
                newName: "IX_MedicinePurchaseOrder_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePurchaseOrders_MedicineId",
                table: "MedicinePurchaseOrder",
                newName: "IX_MedicinePurchaseOrder_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePrescriptions_PrescriptionId",
                table: "MedicinePrescription",
                newName: "IX_MedicinePrescription_PrescriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePrescriptions_MedicineId",
                table: "MedicinePrescription",
                newName: "IX_MedicinePrescription_MedicineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockOrder",
                table: "StockOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrder",
                table: "PurchaseOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prescription",
                table: "Prescription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicineStockOrder",
                table: "MedicineStockOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicinePurchaseOrder",
                table: "MedicinePurchaseOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicinePrescription",
                table: "MedicinePrescription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drug",
                table: "Drug",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescription_Medicines_MedicineId",
                table: "MedicinePrescription",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescription_Prescription_PrescriptionId",
                table: "MedicinePrescription",
                column: "PrescriptionId",
                principalTable: "Prescription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchaseOrder_Medicines_MedicineId",
                table: "MedicinePurchaseOrder",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePurchaseOrder_PurchaseOrder_PurchaseOrderId",
                table: "MedicinePurchaseOrder",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Drug_DrugId",
                table: "Medicines",
                column: "DrugId",
                principalTable: "Drug",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineStockOrder_Medicines_MedicineId",
                table: "MedicineStockOrder",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineStockOrder_StockOrder_StockOrderId",
                table: "MedicineStockOrder",
                column: "StockOrderId",
                principalTable: "StockOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
