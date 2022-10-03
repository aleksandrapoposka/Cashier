using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cashier.Data.Migrations
{
    public partial class AddArticlesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048cdc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "39741fb3-c645-4fb9-ba37-f9ffb6797b8c", "AQAAAAEAACcQAAAAEHRkL9yXByHgD009gjBi3bigSeGxMNqDWCP6P6lGsoVgf27x1Osd8OgHJXicsYC38g==", "47463e53-f05e-447e-9305-d56a2031427c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f0a0654-2588-4a83-851e-4e0883343ca8", "AQAAAAEAACcQAAAAEIMEpUDwD0YabblWAx2bl2DooBAyFZyNk+GrAzMQIkGSgU+y+hhTUkzkLWOCfl3AaQ==", "52f54549-0acb-40a4-8041-328b1e1bbd74" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048cdc1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de2ee8d8-0f3b-41fb-8d49-3f8a96d0dff3", "AQAAAAEAACcQAAAAEObOceXkI+j3Jymk1eoZIAsSs7dcb5XK5SdlnNg89ySb0TdEH8TdY8/QKJbu3Srfdg==", "53684efa-70cf-45db-a9d9-a2d8e6cfade5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07bb1c2b-4254-425b-918d-97421a427452", "AQAAAAEAACcQAAAAEKa2cGpJqVcGpkC+QwDuGc/cROFFVHyos6FaLY8Ju7w3jLueSohKpWXu+wQ8jRJPEw==", "32c9617b-2ae2-42a3-9235-777c34b66fe7" });
        }
    }
}
