using Microsoft.EntityFrameworkCore.Migrations;

namespace ScoringSystem.DAL.Migrations
{
    public partial class updateUserAndHealthReletionships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Healths_HealthId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_HealthId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HealthId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UsersHealth",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    HealthId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersHealth", x => new { x.UserId, x.HealthId });
                    table.ForeignKey(
                        name: "FK_UsersHealth_Healths_HealthId",
                        column: x => x.HealthId,
                        principalTable: "Healths",
                        principalColumn: "HealthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersHealth_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersHealth_HealthId",
                table: "UsersHealth",
                column: "HealthId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersHealth");

            migrationBuilder.AddColumn<int>(
                name: "HealthId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_HealthId",
                table: "Users",
                column: "HealthId",
                unique: true,
                filter: "[HealthId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Healths_HealthId",
                table: "Users",
                column: "HealthId",
                principalTable: "Healths",
                principalColumn: "HealthId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
