using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinance.Repo.Migrations
{
    public partial class FixSpecialPersentages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SpecialPercentagePeriod",
                newName: "SpecialPercentagePeriod",
                newSchema: "MyFin");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "MyFin",
                table: "SpecialPercentagePeriod",
                nullable: false,
                defaultValueSql: "newsequentialid()",
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SpecialPercentagePeriod",
                schema: "MyFin",
                newName: "SpecialPercentagePeriod");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "SpecialPercentagePeriod",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "newsequentialid()");
        }
    }
}
