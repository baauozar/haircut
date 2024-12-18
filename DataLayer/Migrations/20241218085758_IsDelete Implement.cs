using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class IsDeleteImplement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "BeautyItems");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "BeautyCategories");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "BeautyCardInfos");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HairCutSupServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HaircutServicesCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HaircutServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HaircutMenuItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HaircutMenuCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BeautyItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BeautyCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HairCutSupServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HaircutServicesCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HaircutServices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HaircutMenuItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HaircutMenuCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BeautyItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BeautyCategories");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "BeautyItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "BeautyCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "BeautyCardInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
