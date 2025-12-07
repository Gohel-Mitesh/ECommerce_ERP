using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_ERP.Migrations
{
    /// <inheritdoc />
    public partial class ProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsMaster",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProductSKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductBarcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductDiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductChargeTax = table.Column<bool>(type: "bit", nullable: false),
                    ProductInStock = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductStatus = table.Column<int>(type: "int", nullable: false),
                    ProductTags = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsMaster", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantsDetails",
                columns: table => new
                {
                    VariantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VariantSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VariantColorHex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VariantQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantsDetails", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariantsDetails_ProductsMaster_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductsMaster",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariantPhotos",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VariantId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariantPhotos", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_ProductVariantPhotos_ProductVariantsDetails_VariantId",
                        column: x => x.VariantId,
                        principalTable: "ProductVariantsDetails",
                        principalColumn: "VariantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantPhotos_VariantId",
                table: "ProductVariantPhotos",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantsDetails_ProductId",
                table: "ProductVariantsDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVariantPhotos");

            migrationBuilder.DropTable(
                name: "ProductVariantsDetails");

            migrationBuilder.DropTable(
                name: "ProductsMaster");
        }
    }
}
