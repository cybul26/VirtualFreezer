using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountConfirmation.Api.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Verifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    VerificationHash = table.Column<string>(type: "text", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verifications");
        }
    }
}
