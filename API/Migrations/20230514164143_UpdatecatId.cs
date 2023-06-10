using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Website.Migrations
{
    public partial class UpdatecatId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_categoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "categoryId1",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "categoryId",
                table: "Products",
                type: "NVARCHAR2(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_categoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_categoryId",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "categoryId",
                table: "Products",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "categoryId1",
                table: "Products",
                type: "NVARCHAR2(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId1",
                table: "Products",
                column: "categoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_categoryId1",
                table: "Products",
                column: "categoryId1",
                principalTable: "Categories",
                principalColumn: "categoryId");
        }
    }
}
