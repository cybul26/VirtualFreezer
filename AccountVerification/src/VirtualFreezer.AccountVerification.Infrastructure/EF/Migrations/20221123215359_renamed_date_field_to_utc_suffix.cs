using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Migrations
{
    public partial class renamed_date_field_to_utc_suffix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidUntil",
                table: "Verifications",
                newName: "ValidUntilUtc");

            migrationBuilder.RenameColumn(
                name: "When",
                table: "Resends",
                newName: "WhenUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidUntilUtc",
                table: "Verifications",
                newName: "ValidUntil");

            migrationBuilder.RenameColumn(
                name: "WhenUtc",
                table: "Resends",
                newName: "When");
        }
    }
}
