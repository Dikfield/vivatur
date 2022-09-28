using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class VivaphotosUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Abouts",
                newName: "Title");

            migrationBuilder.AddColumn<bool>(
                name: "VivaPic",
                table: "VivaPhotos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VivaInfo",
                table: "Abouts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VivaPic",
                table: "VivaPhotos");

            migrationBuilder.DropColumn(
                name: "VivaInfo",
                table: "Abouts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Abouts",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
