using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApp.Migrations
{
    /// <inheritdoc />
    public partial class alterOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tOrder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EstimatedTotal = table.Column<double>(type: "float", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDelivered = table.Column<bool>(type: "bit", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tOrder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tOrderItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tOrderItem_tOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tOrderShippingAddress",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tOrderShippingAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tOrderShippingAddress_tOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tOrder_UserId",
                table: "tOrder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tOrderItem_OrderId",
                table: "tOrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tOrderShippingAddress_OrderId",
                table: "tOrderShippingAddress",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tOrderItem");

            migrationBuilder.DropTable(
                name: "tOrderShippingAddress");

            migrationBuilder.DropTable(
                name: "tOrder");
        }
    }
}
