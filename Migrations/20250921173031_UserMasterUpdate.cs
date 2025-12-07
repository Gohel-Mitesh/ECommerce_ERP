using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce_ERP.Migrations
{
    /// <inheritdoc />
    public partial class UserMasterUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPassword",
                table: "UserMaster");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserMaster",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserMaster");

            migrationBuilder.AddColumn<string>(
                name: "UserPassword",
                table: "UserMaster",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
