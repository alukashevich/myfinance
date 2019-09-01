using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFinance.Repo.Migrations
{
    public partial class AddSpecialPersentages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialPercentagePeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstMonth = table.Column<int>(nullable: false),
                    LastMonth = table.Column<int>(nullable: false),
                    MonthPercentage = table.Column<decimal>(nullable: false),
                    CreditId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialPercentagePeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialPercentagePeriod_Credit_CreditId",
                        column: x => x.CreditId,
                        principalSchema: "MyFin",
                        principalTable: "Credit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialPercentagePeriod_CreditId",
                table: "SpecialPercentagePeriod",
                column: "CreditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialPercentagePeriod");
        }
    }
}
