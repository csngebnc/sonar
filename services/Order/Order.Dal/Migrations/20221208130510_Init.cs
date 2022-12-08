using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.Dal.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lockers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_StreetAndNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Other = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lockers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sender_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sender_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sender_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receiver_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receiver_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Receiver_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size_Width = table.Column<int>(type: "int", nullable: true),
                    Size_Height = table.Column<int>(type: "int", nullable: true),
                    Size_Depth = table.Column<int>(type: "int", nullable: true),
                    Size_Weight = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LockerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PickupAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress_County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress_StreetAndNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PickupAddress_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress_County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress_StreetAndNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CashOnDeliveryPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_Lockers_LockerId",
                        column: x => x.LockerId,
                        principalTable: "Lockers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraOptionParcel",
                columns: table => new
                {
                    ExtraOptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraOptionParcel", x => new { x.ExtraOptionsId, x.ParcelsId });
                    table.ForeignKey(
                        name: "FK_ExtraOptionParcel_ExtraOptions_ExtraOptionsId",
                        column: x => x.ExtraOptionsId,
                        principalTable: "ExtraOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraOptionParcel_Parcels_ParcelsId",
                        column: x => x.ParcelsId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOptionParcel_ParcelsId",
                table: "ExtraOptionParcel",
                column: "ParcelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_LockerId",
                table: "Parcels",
                column: "LockerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraOptionParcel");

            migrationBuilder.DropTable(
                name: "ExtraOptions");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Lockers");
        }
    }
}
