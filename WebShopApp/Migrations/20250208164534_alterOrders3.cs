using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApp.Migrations
{
    /// <inheritdoc />
    public partial class alterOrders3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tOrderItem_tUserOrder_OrderId",
                table: "tOrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tUserOrder_AspNetUsers_UserId",
                table: "tUserOrder");

            migrationBuilder.DropTable(
                name: "tShippingAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tUserOrder",
                table: "tUserOrder");

            migrationBuilder.RenameTable(
                name: "tUserOrder",
                newName: "tOrder");

            migrationBuilder.RenameIndex(
                name: "IX_tUserOrder_UserId",
                table: "tOrder",
                newName: "IX_tOrder_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tOrder",
                table: "tOrder",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tOrderShippingAddress",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_tOrderShippingAddress_OrderId",
                table: "tOrderShippingAddress",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tOrder_AspNetUsers_UserId",
                table: "tOrder",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tOrderItem_tOrder_OrderId",
                table: "tOrderItem",
                column: "OrderId",
                principalTable: "tOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tOrder_AspNetUsers_UserId",
                table: "tOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_tOrderItem_tOrder_OrderId",
                table: "tOrderItem");

            migrationBuilder.DropTable(
                name: "tOrderShippingAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tOrder",
                table: "tOrder");

            migrationBuilder.RenameTable(
                name: "tOrder",
                newName: "tUserOrder");

            migrationBuilder.RenameIndex(
                name: "IX_tOrder_UserId",
                table: "tUserOrder",
                newName: "IX_tUserOrder_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tUserOrder",
                table: "tUserOrder",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tShippingAddress",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tShippingAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tShippingAddress_tUserOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tUserOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tShippingAddress_OrderId",
                table: "tShippingAddress",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_tOrderItem_tUserOrder_OrderId",
                table: "tOrderItem",
                column: "OrderId",
                principalTable: "tUserOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tUserOrder_AspNetUsers_UserId",
                table: "tUserOrder",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
