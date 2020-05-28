using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringSystem.DAL.Migrations
{
    public partial class addedDateToHealthTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnalizDate",
                table: "Healths",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnalizDate",
                table: "Healths");
        }
    }
}
