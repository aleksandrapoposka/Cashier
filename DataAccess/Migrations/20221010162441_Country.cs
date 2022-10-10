using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsoNumericCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alpha2Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alpha3Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048c222",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1bd2007c-e6da-44c4-8601-c66a32370fed", "AQAAAAEAACcQAAAAEIuJ2xecqVO8pcqqjXZ2xYXwsbOz6xo+qD1NgOHm5wC7mkYZjKAXNhOXiKqqElJvrw==", "c0194f48-4511-4583-b404-6c23ee64244a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ec7066c-3857-4c95-8971-46c6d2dbad19", "AQAAAAEAACcQAAAAEKS9MDvBCsq047mH4B9u0mtK9UUGLGU3Laa/9wjcp1B632RLpOBddmY/wWdwZX5E8w==", "a8e842bb-2c14-4faf-88d9-080b94183681" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Articles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445123-a24d-4543-a6c6-9443d048c222",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df5c0b1e-4b45-4660-963c-43118adfe428", "AQAAAAEAACcQAAAAEMU6sUFhlefQrFAFXRzwCNlDJoP+Osv4R16/IltkkkiEe0d1NRBp9fVQi2+sDHsWiQ==", "960d6646-833d-4669-9b78-3d9601ad1eac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdd964c3-9ed1-4e5e-8bec-d92ed4360a47", "AQAAAAEAACcQAAAAEG94kOlpbcB80i1rZD+50UA1TLRz+ZUHKl6BpNoJzFa2R7K+nUZeGIbqzXW3lle1bg==", "1841a519-67c9-4e95-9c33-208908b6e20f" });
        }
    }
}
