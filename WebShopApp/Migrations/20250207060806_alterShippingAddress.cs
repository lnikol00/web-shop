using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopApp.Migrations
{
    /// <inheritdoc />
    public partial class alterShippingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_tUserOrder_OrderId",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.RenameTable(
                name: "ShippingAddresses",
                newName: "tShippingAddress");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddresses_OrderId",
                table: "tShippingAddress",
                newName: "IX_tShippingAddress_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tShippingAddress",
                table: "tShippingAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tShippingAddress_tUserOrder_OrderId",
                table: "tShippingAddress",
                column: "OrderId",
                principalTable: "tUserOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tShippingAddress_tUserOrder_OrderId",
                table: "tShippingAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tShippingAddress",
                table: "tShippingAddress");

            migrationBuilder.RenameTable(
                name: "tShippingAddress",
                newName: "ShippingAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_tShippingAddress_OrderId",
                table: "ShippingAddresses",
                newName: "IX_ShippingAddresses_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_tUserOrder_OrderId",
                table: "ShippingAddresses",
                column: "OrderId",
                principalTable: "tUserOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
