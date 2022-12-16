using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class feedbackUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeedbackPhotoId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeedbackPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackPhoto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FeedbackPhotoId",
                table: "Feedbacks",
                column: "FeedbackPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackPhoto_FeedbackPhotoId",
                table: "Feedbacks",
                column: "FeedbackPhotoId",
                principalTable: "FeedbackPhoto",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackPhoto_FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.DropTable(
                name: "FeedbackPhoto");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackPhotoId",
                table: "Feedbacks");
        }
    }
}
