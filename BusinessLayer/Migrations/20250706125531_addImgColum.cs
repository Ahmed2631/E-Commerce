using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addImgColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryImg",
                table: "TbCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImg",
                table: "TbCategories");
        }
    }
}
