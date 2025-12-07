using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRelationToProductsMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductsMaster_CategoryId",
                table: "ProductsMaster",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsMaster_CategoryMaster_CategoryId",
                table: "ProductsMaster",
                column: "CategoryId",
                principalTable: "CategoryMaster",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsMaster_CategoryMaster_CategoryId",
                table: "ProductsMaster");

            migrationBuilder.DropIndex(
                name: "IX_ProductsMaster_CategoryId",
                table: "ProductsMaster");
        }
    }
}
