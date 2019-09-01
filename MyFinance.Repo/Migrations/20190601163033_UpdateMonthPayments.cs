using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinance.Repo.Migrations
{
    public partial class UpdateMonthPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Persenteges",
                schema: "MyFin",
                table: "MonthPayment",
                newName: "PersentegesPyment");

            migrationBuilder.AddColumn<decimal>(
                name: "Persentege",
                schema: "MyFin",
                table: "MonthPayment",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Persentege",
                schema: "MyFin",
                table: "MonthPayment");

            migrationBuilder.RenameColumn(
                name: "PersentegesPyment",
                schema: "MyFin",
                table: "MonthPayment",
                newName: "Persenteges");
        }
    }
}
