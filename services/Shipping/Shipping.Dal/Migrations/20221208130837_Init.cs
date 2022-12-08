using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shipping.Dal.Migrations
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
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Address_Other = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lockers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ParcelEventBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelEventBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParcelEventBase_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Type", "Username" },
                values: new object[] { new Guid("85b4a2a3-aa22-40e4-9b91-c72e96ab8e08"), 0, "csngebnc" });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraOptionParcel_ParcelsId",
                table: "ExtraOptionParcel",
                column: "ParcelsId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelEventBase_ParcelId",
                table: "ParcelEventBase",
                column: "ParcelId");

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
                name: "ParcelEventBase");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ExtraOptions");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Lockers");
        }
    }
}
