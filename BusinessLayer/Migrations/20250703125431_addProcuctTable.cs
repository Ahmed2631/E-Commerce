using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addProcuctTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryFactor",
                table: "TbCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false);

            migrationBuilder.CreateTable(
                name: "TbPoducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salesprices = table.Column<decimal>(type: "decimal(18,2)", maxLength: 5, nullable: false),
                    purchaseprices = table.Column<decimal>(type: "decimal(18,2)", maxLength: 5, nullable: false),
                    Profite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPoducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbPoducts_TbCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TbCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbPoducts_CategoryId",
                table: "TbPoducts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbPoducts");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryFactor",
                table: "TbCategories",
                type: "nvarchar(max)",
                nullable: false);
        }
    }
}
