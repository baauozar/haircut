using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class namefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HaircutMenuItems_HaircutMenuCategories_MenuCategoryId",
                table: "HaircutMenuItems");

            migrationBuilder.RenameColumn(
                name: "MenuCategoryId",
                table: "HaircutMenuItems",
                newName: "HaircutMenuCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HaircutMenuItems_MenuCategoryId",
                table: "HaircutMenuItems",
                newName: "IX_HaircutMenuItems_HaircutMenuCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_HaircutMenuItems_HaircutMenuCategories_HaircutMenuCategoryId",
                table: "HaircutMenuItems",
                column: "HaircutMenuCategoryId",
                principalTable: "HaircutMenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HaircutMenuItems_HaircutMenuCategories_HaircutMenuCategoryId",
                table: "HaircutMenuItems");

            migrationBuilder.RenameColumn(
                name: "HaircutMenuCategoryId",
                table: "HaircutMenuItems",
                newName: "MenuCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HaircutMenuItems_HaircutMenuCategoryId",
                table: "HaircutMenuItems",
                newName: "IX_HaircutMenuItems_MenuCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_HaircutMenuItems_HaircutMenuCategories_MenuCategoryId",
                table: "HaircutMenuItems",
                column: "MenuCategoryId",
                principalTable: "HaircutMenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
