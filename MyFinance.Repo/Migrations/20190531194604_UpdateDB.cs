using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinance.Repo.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Persenteges",
                schema: "MyFin",
                table: "MonthPayment",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                schema: "MyFin",
                table: "Credit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Persenteges",
                schema: "MyFin",
                table: "MonthPayment");

            migrationBuilder.DropColumn(
                name: "IsStarted",
                schema: "MyFin",
                table: "Credit");
        }
    }
}
