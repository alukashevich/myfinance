using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinance.Repo.Migrations
{
    public partial class UpdateSpecialPersentages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstMonth",
                table: "SpecialPercentagePeriod");

            migrationBuilder.RenameColumn(
                name: "LastMonth",
                table: "SpecialPercentagePeriod",
                newName: "MonthCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthCount",
                table: "SpecialPercentagePeriod",
                newName: "LastMonth");

            migrationBuilder.AddColumn<int>(
                name: "FirstMonth",
                table: "SpecialPercentagePeriod",
                nullable: false,
                defaultValue: 0);
        }
    }
}
