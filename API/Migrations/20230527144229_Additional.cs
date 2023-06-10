using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Website.Migrations
{
    public partial class Additional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "orderDetailsId",
                table: "Products",
                type: "NVARCHAR2(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "orderDetails",
                columns: table => new
                {
                    orderDetailsId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    orderId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    productId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    name = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    price = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    total = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => x.orderDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "userCards",
                columns: table => new
                {
                    userCardId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    userId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    cardName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    cardNumber = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    expiryMonth = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    expiryYear = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    cvv = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userCards", x => x.userCardId);
                    table.ForeignKey(
                        name: "FK_userCards_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    orderId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    userId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    userName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    dateTime = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    state = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    total = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    orderDetailsId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderId);
                    table.ForeignKey(
                        name: "FK_orders_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orders_orderDetails_orderDetailsId",
                        column: x => x.orderDetailsId,
                        principalTable: "orderDetails",
                        principalColumn: "orderDetailsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_orderDetailsId",
                table: "Products",
                column: "orderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_orderDetailsId",
                table: "orders",
                column: "orderDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_userId",
                table: "orders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_userCards_userId",
                table: "userCards",
                column: "userId",
                unique: true,
                filter: "\"userId\" IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "categoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_orderDetails_orderDetailsId",
                table: "Products",
                column: "orderDetailsId",
                principalTable: "orderDetails",
                principalColumn: "orderDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_orderDetails_orderDetailsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "userCards");

            migrationBuilder.DropTable(
                name: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Products_orderDetailsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "orderDetailsId",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "categoryId");
        }
    }
}
