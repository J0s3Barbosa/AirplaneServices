using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirplaneServices.Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirPlaneModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirPlaneModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirPlane",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    ModelId = table.Column<Guid>(nullable: false),
                    NumberOfPassengers = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirPlane", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirPlane_AirPlaneModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "AirPlaneModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AirPlaneModel",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"), "Airbus A300B1" },
                    { new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc2"), "Airbus A319" },
                    { new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"), "Boeing 737-100" },
                    { new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc4"), "Boeing CRJ-100" }
                });

            migrationBuilder.InsertData(
                table: "AirPlane",
                columns: new[] { "Id", "Code", "CreationDate", "ModelId", "NumberOfPassengers" },
                values: new object[] { new Guid("b413cfc0-f53a-4765-9430-3912efcd79cb"), "1", new DateTime(2020, 1, 23, 0, 19, 29, 802, DateTimeKind.Local).AddTicks(1536), new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc1"), 111 });

            migrationBuilder.InsertData(
                table: "AirPlane",
                columns: new[] { "Id", "Code", "CreationDate", "ModelId", "NumberOfPassengers" },
                values: new object[] { new Guid("a714554f-f363-42f1-b41a-81ee85186622"), "3B", new DateTime(2020, 1, 23, 0, 19, 29, 807, DateTimeKind.Local).AddTicks(8112), new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc3"), 167 });

            migrationBuilder.InsertData(
                table: "AirPlane",
                columns: new[] { "Id", "Code", "CreationDate", "ModelId", "NumberOfPassengers" },
                values: new object[] { new Guid("a714554f-f363-42f1-b41a-81ee85186661"), "4", new DateTime(2020, 1, 23, 0, 19, 29, 807, DateTimeKind.Local).AddTicks(8280), new Guid("7f430a38-a6b2-4a8f-96d5-801725dfdfc4"), 117 });

            migrationBuilder.CreateIndex(
                name: "IX_AirPlane_ModelId",
                table: "AirPlane",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirPlane");

            migrationBuilder.DropTable(
                name: "AirPlaneModel");
        }
    }
}
