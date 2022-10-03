using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cashier.Data.Migrations
{
    public partial class CreateSuperAdminFilip3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb1", 0, "728f80d2-919f-4ba1-82f6-8d3b0dbee8d6", new DateTime(1995, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "filip@admin.com", false, "Filip", "Cvetkovski", false, null, "FILIP@ADMIN.COM", "FILIP@ADMIN.COM", "AQAAAAEAACcQAAAAELYp9c7y02KsaDpQmJV/HX9KXp1pp1i1v+swwWV+qGdrEBQM/T5uwd05X69ub7x3cQ==", "077511123", false, "5187ac11-76ca-4dd3-b3ea-3af02968fbea", false, "filip@admin.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "92f73f20-6020-417a-9344-6588b13a4374", new DateTime(1995, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "filip@admin.com", false, "Filip", "Cvetkovski", false, null, "FILIP@ADMIN.COM", "FILIP@ADMIN.COM", "AQAAAAEAACcQAAAAENGP1ALckD3TpI2pqFmhfW6mqMflfR+RGCaBYgtti7+v18tUn40BvOFFXFXxEsOOww==", "077511123", false, "1841f862-4f4b-49ef-a373-b1c9ff8a6b9c", false, "filip@admin.com" });
        }
    }
}
