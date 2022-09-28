using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cashier.Data.Migrations
{
    public partial class SeedOtherRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1fdff5e-9818-4515-9862-fc7a13de65b9", "a1fdff5e-9818-4515-9862-fc7a13de65b9", "Cashier", "CASHIER" },
                    { "e9bfcae5-cc66-4512-bbeb-7536c527221b", "e9bfcae5-cc66-4512-bbeb-7536c527221b", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048cdc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f15d8df0-2a6e-4b34-a592-7f3d6cfe183a", "AQAAAAEAACcQAAAAEP75aTAiGB0dQoZ4csn2Q7DDMwN+NHta3t3idBh0/EgeAxMpLeNeBqMGAmnQpO4XTg==", "7c66959e-cd30-40c8-bf7f-f9d6f4e0fafe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48b4857d-8dcf-480c-b444-a3060b0d4514", "AQAAAAEAACcQAAAAEHphXc8pEQbtumQi2MCgaXYf1iCkr3E8yMH7xlWwNXo9+NAU4cebUWngtMUZf+QxjQ==", "273c71ef-baca-4f14-847b-2722f713e649" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1fdff5e-9818-4515-9862-fc7a13de65b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9bfcae5-cc66-4512-bbeb-7536c527221b");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048cdc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00a434a4-2b57-4086-b133-7a8a57355752", "AQAAAAEAACcQAAAAECMIwm+gyf30L5xtBkVq0hGP7rm2+KjUqhQz9EuOYnfc/fI5dtTVT0FLf4JXQYdtuw==", "96b30cff-c39c-4065-b531-05b895dd8053" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e36e1704-4e3a-43c6-8bfb-e4d941cf6ae4", "AQAAAAEAACcQAAAAEAJAtovocbM6MLz9QdfIMJsr0qgPOsIBxVZl/9shtfIQ5VLem8oFdH/hW4tTjs/gyg==", "46fef8e6-31f4-4567-88c6-b8bfc263fb58" });
        }
    }
}
