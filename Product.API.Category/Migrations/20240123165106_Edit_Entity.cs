using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.API.Category.Migrations
{
    /// <inheritdoc />
    public partial class Edit_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryEntityCatId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryEntityCatId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryEntityCatId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryEntityCatId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryEntityCatId",
                table: "Categories",
                column: "CategoryEntityCatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryEntityCatId",
                table: "Categories",
                column: "CategoryEntityCatId",
                principalTable: "Categories",
                principalColumn: "CatId");
        }
    }
}
