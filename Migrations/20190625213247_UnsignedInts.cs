using Microsoft.EntityFrameworkCore.Migrations;

namespace GestiónDeMedicamentos.Migrations
{
    public partial class UnsignedInts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Drugs_DrugId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DrugId",
                table: "Medicines");

            migrationBuilder.AlterColumn<long>(
                name: "Stock",
                table: "Medicines",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "DrugId",
                table: "Medicines",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DrugId1",
                table: "Medicines",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                table: "MedicinePurchaseOrders",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "Quantity",
                table: "MedicinePrescriptions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DrugId1",
                table: "Medicines",
                column: "DrugId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Drugs_DrugId1",
                table: "Medicines",
                column: "DrugId1",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Drugs_DrugId1",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DrugId1",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DrugId1",
                table: "Medicines");

            migrationBuilder.AlterColumn<int>(
                name: "Stock",
                table: "Medicines",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "DrugId",
                table: "Medicines",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "MedicinePurchaseOrders",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "MedicinePrescriptions",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DrugId",
                table: "Medicines",
                column: "DrugId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Drugs_DrugId",
                table: "Medicines",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
