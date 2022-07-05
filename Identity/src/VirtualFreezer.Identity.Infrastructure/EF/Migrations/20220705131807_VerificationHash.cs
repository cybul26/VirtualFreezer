using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualFreezer.Identity.Infrastructure.EF.Migrations
{
    public partial class VerificationHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VerificationHash",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationHash",
                table: "Users");
        }
    }
}
