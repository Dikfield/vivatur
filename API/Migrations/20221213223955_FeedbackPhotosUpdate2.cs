using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class FeedbackPhotosUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_FeedbackPhotos_FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackPhotoId",
                table: "Feedbacks");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "FeedbackPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackPhotos_FeedbackId",
                table: "FeedbackPhotos",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackPhotos_Feedbacks_FeedbackId",
                table: "FeedbackPhotos",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackPhotos_Feedbacks_FeedbackId",
                table: "FeedbackPhotos");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackPhotos_FeedbackId",
                table: "FeedbackPhotos");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "FeedbackPhotos");

            migrationBuilder.AddColumn<int>(
                name: "FeedbackPhotoId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_FeedbackPhotoId",
                table: "Feedbacks",
                column: "FeedbackPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_FeedbackPhotos_FeedbackPhotoId",
                table: "Feedbacks",
                column: "FeedbackPhotoId",
                principalTable: "FeedbackPhotos",
                principalColumn: "Id");
        }
    }
}
