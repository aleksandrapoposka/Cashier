using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class CountryINT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Country",
                table: "Articles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048c222",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4f5e18ab-9278-48ef-b7cc-b541b85e0c6b", "AQAAAAEAACcQAAAAEEVqEL93sdRBHdjFhOgJ1c0h+PfF5FKCCATkqRjqBj1cFAHZwtZYw1Ffa+ysUCAr0Q==", "f8dc9ea9-22d7-4123-9959-646a327faca3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "410eb39e-ed7e-43af-bc74-df772366435c", "AQAAAAEAACcQAAAAEIEbM/ej8u0sOaPrFKBM9dr5GlX9FP4FT2G8lz3UWWPnRDa3UUIvwHmdNhE0xoMLwQ==", "c66f7f8a-3b57-4b06-82f6-1ca6ac943a61" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048c222",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87db61df-457b-45d8-b2ea-cc952b34faf1", "AQAAAAEAACcQAAAAEFGziqO6esVuXfzfYdJp++XUhV5i/nV7cJH5Ot4Sz2JMFde+JQt9CIUxcEhPncU1ow==", "ae2511f3-d7b7-46d8-8521-7cdc889633cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55dd93c3-88eb-4282-8536-8f7094465bdd", "AQAAAAEAACcQAAAAEDj+r0lOPY9unqYSfzeTBXiRmVh1W8Hq6h7x3Uosy4v0X9GsQMY2lBStPwj26gm0vw==", "e26420a3-3ab4-43fc-8826-a18995baff7a" });
        }
    }
}
