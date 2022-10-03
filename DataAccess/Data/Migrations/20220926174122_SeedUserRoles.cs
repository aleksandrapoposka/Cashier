using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cashier.Data.Migrations
{
    public partial class SeedUserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e36e1704-4e3a-43c6-8bfb-e4d941cf6ae4", "AQAAAAEAACcQAAAAEAJAtovocbM6MLz9QdfIMJsr0qgPOsIBxVZl/9shtfIQ5VLem8oFdH/hW4tTjs/gyg==", "46fef8e6-31f4-4567-88c6-b8bfc263fb58" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445123-a24d-4543-a6c6-9443d048cdc1", 0, "00a434a4-2b57-4086-b133-7a8a57355752", new DateTime(1995, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ApplicationUser", "aleksandra@admin.com", false, "Aleksandra", "Poposka", false, null, "ALEKSANDRA@ADMIN.COM", "ALEKSANDRA@ADMIN.COM", "AQAAAAEAACcQAAAAECMIwm+gyf30L5xtBkVq0hGP7rm2+KjUqhQz9EuOYnfc/fI5dtTVT0FLf4JXQYdtuw==", "077511166", false, "96b30cff-c39c-4065-b531-05b895dd8053", false, "aleksandra@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445123-a24d-4543-a6c6-9443d048cdc1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445123-a24d-4543-a6c6-9443d048cdc1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2c5e174e-3b0e-446f-86af-483d56fd7210", "8e445865-a24d-4543-a6c6-9443d048cdb1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048cdc1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "728f80d2-919f-4ba1-82f6-8d3b0dbee8d6", "AQAAAAEAACcQAAAAELYp9c7y02KsaDpQmJV/HX9KXp1pp1i1v+swwWV+qGdrEBQM/T5uwd05X69ub7x3cQ==", "5187ac11-76ca-4dd3-b3ea-3af02968fbea" });
        }
    }
}
