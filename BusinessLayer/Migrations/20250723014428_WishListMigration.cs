using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLayer.Migrations
{
    /// <inheritdoc />
    public partial class WishListMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WishlistCartId",
                table: "TbPoducts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "TbOrderHeader",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "TbOrderHeader",
                type: "nvarchar(15)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "TbOrderHeader",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Carrier",
                table: "TbOrderHeader",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Adress",
                table: "TbOrderHeader",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "WishlistCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistCart", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbPoducts_WishlistCartId",
                table: "TbPoducts",
                column: "WishlistCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbPoducts_WishlistCart_WishlistCartId",
                table: "TbPoducts",
                column: "WishlistCartId",
                principalTable: "WishlistCart",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbPoducts_WishlistCart_WishlistCartId",
                table: "TbPoducts");

            migrationBuilder.DropTable(
                name: "WishlistCart");

            migrationBuilder.DropIndex(
                name: "IX_TbPoducts_WishlistCartId",
                table: "TbPoducts");

            migrationBuilder.DropColumn(
                name: "WishlistCartId",
                table: "TbPoducts");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "TbOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "TbOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "TbOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Carrier",
                table: "TbOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Adress",
                table: "TbOrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);
        }
    }
}
