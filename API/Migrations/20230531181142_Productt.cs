using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Website.Migrations
{
    public partial class Productt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Products",
                newName: "updatedAt");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "createdAt",
                table: "Products",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "discount",
                table: "Products",
                type: "DECIMAL(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "orders",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "orderDetails",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "orderDetails",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.CreateTable(
                name: "productImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    productId = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    image = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productImages_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productImages_productId",
                table: "productImages",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productImages");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Products",
                newName: "ReleaseDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "orders",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total",
                table: "orderDetails",
                type: "DECIMAL(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "orderDetails",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}
