using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPremaryKeyForWislList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "WishlistCart",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistCart_ApplicationUserId",
                table: "WishlistCart",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistCart_AspNetUsers_ApplicationUserId",
                table: "WishlistCart",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistCart_AspNetUsers_ApplicationUserId",
                table: "WishlistCart");

            migrationBuilder.DropIndex(
                name: "IX_WishlistCart_ApplicationUserId",
                table: "WishlistCart");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WishlistCart");
        }
    }
}
