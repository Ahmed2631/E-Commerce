using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLayer.Migrations
{
    /// <inheritdoc />
    public partial class WishListSMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "WishlistCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "TbOrderHeader",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.CreateIndex(
                name: "IX_WishlistCart_ProductId",
                table: "WishlistCart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistCart_TbPoducts_ProductId",
                table: "WishlistCart",
                column: "ProductId",
                principalTable: "TbPoducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistCart_TbPoducts_ProductId",
                table: "WishlistCart");

            migrationBuilder.DropIndex(
                name: "IX_WishlistCart_ProductId",
                table: "WishlistCart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "WishlistCart");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "TbOrderHeader",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
