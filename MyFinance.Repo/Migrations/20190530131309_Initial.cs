using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinance.Repo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MyFin");

            migrationBuilder.CreateTable(
                name: "Credit",
                schema: "MyFin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Amount = table.Column<decimal>(nullable: false),
                    BasePercenteges = table.Column<decimal>(nullable: false),
                    MonthsCount = table.Column<int>(nullable: false),
                    CreditType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthPayment",
                schema: "MyFin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    Number = table.Column<int>(nullable: false),
                    Payment = table.Column<decimal>(nullable: false),
                    CreditId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthPayment_Credit_CreditId",
                        column: x => x.CreditId,
                        principalSchema: "MyFin",
                        principalTable: "Credit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthPayment_CreditId",
                schema: "MyFin",
                table: "MonthPayment",
                column: "CreditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthPayment",
                schema: "MyFin");

            migrationBuilder.DropTable(
                name: "Credit",
                schema: "MyFin");
        }
    }
}
