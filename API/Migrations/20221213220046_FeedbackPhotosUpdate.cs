using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class FeedbackPhotosUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackPhoto_FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackPhoto",
                table: "FeedbackPhoto");

            migrationBuilder.RenameTable(
                name: "FeedbackPhoto",
                newName: "FeedbackPhotos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackPhotos",
                table: "FeedbackPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackPhotos_FeedbackPhotoId",
                table: "Feedbacks",
                column: "FeedbackPhotoId",
                principalTable: "FeedbackPhotos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackPhotos_FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeedbackPhotos",
                table: "FeedbackPhotos");

            migrationBuilder.RenameTable(
                name: "FeedbackPhotos",
                newName: "FeedbackPhoto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeedbackPhoto",
                table: "FeedbackPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackPhoto_FeedbackPhotoId",
                table: "Feedbacks",
                column: "FeedbackPhotoId",
                principalTable: "FeedbackPhoto",
                principalColumn: "Id");
        }
    }
}
