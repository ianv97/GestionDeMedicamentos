using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GestiónDeMedicamentos.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "DrugId",
                table: "Medicines",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DrugId",
                table: "Medicines",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Drugs_DrugId",
                table: "Medicines",
                column: "DrugId",
                principalTable: "Drugs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Drugs_DrugId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DrugId",
                table: "Medicines");

            migrationBuilder.AlterColumn<long>(
                name: "DrugId",
                table: "Medicines",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DrugId1",
                table: "Medicines",
                nullable: true);

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
    }
}
