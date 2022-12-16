using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class DatabaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DescriptionPhotos");

            migrationBuilder.DropTable(
                name: "PromotionDescriptionPhotos");

            migrationBuilder.DropTable(
                name: "DestinationDescriptions");

            migrationBuilder.DropTable(
                name: "PromotionDescriptions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Promotions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Destinations");

            migrationBuilder.CreateTable(
                name: "DestinationDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationDescriptions_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDescriptions_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionId = table.Column<int>(type: "int", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionPhotos_DestinationDescriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "DestinationDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDescriptionPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromotionDescriptionId = table.Column<int>(type: "int", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDescriptionPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionDescriptionPhotos_PromotionDescriptions_PromotionDescriptionId",
                        column: x => x.PromotionDescriptionId,
                        principalTable: "PromotionDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionPhotos_DescriptionId",
                table: "DescriptionPhotos",
                column: "DescriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DestinationDescriptions_DestinationId",
                table: "DestinationDescriptions",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDescriptionPhotos_PromotionDescriptionId",
                table: "PromotionDescriptionPhotos",
                column: "PromotionDescriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDescriptions_PromotionId",
                table: "PromotionDescriptions",
                column: "PromotionId");
        }
    }
}
