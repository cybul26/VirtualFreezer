using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Migrations
{
    public partial class verification_changed_some_properties_to_private_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeUtc",
                table: "Resends",
                newName: "When");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "When",
                table: "Resends",
                newName: "DateTimeUtc");
        }
    }
}
