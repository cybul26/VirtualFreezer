using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualFreezer.AccountVerification.Infrastructure.EF.Migrations
{
    public partial class email_as_key_and_resends_made : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Verifications",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Verifications");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Verifications",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ResendsMade",
                table: "Verifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Verifications",
                table: "Verifications",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_Email",
                table: "Verifications",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Verifications_VerificationHash",
                table: "Verifications",
                column: "VerificationHash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Verifications",
                table: "Verifications");

            migrationBuilder.DropIndex(
                name: "IX_Verifications_Email",
                table: "Verifications");

            migrationBuilder.DropIndex(
                name: "IX_Verifications_VerificationHash",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Verifications");

            migrationBuilder.DropColumn(
                name: "ResendsMade",
                table: "Verifications");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Verifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Verifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Verifications",
                table: "Verifications",
                column: "Id");
        }
    }
}
