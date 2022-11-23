using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Migrations
{
    public partial class resends_added_to_verification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResendsMade",
                table: "Verifications");

            migrationBuilder.CreateTable(
                name: "Resends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateTimeUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resends_Verifications_Email",
                        column: x => x.Email,
                        principalTable: "Verifications",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resends_Email",
                table: "Resends",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resends");

            migrationBuilder.AddColumn<int>(
                name: "ResendsMade",
                table: "Verifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
