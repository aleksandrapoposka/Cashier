using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Countries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { "87db61df-457b-45d8-b2ea-cc952b34faf1", "AQAAAAEAACcQAAAAEFGziqO6esVuXfzfYdJp++XUhV5i/nV7cJH5Ot4Sz2JMFde+JQt9CIUxcEhPncU1ow==", "ae2511f3-d7b7-46d8-8521-7cdc889633cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048c111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55dd93c3-88eb-4282-8536-8f7094465bdd", "AQAAAAEAACcQAAAAEDj+r0lOPY9unqYSfzeTBXiRmVh1W8Hq6h7x3Uosy4v0X9GsQMY2lBStPwj26gm0vw==", "e26420a3-3ab4-43fc-8826-a18995baff7a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

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
