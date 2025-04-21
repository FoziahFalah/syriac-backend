using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCoverPhotoRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sources_Attachments_CoverPhotoId1",
                table: "Sources");

            migrationBuilder.DropIndex(
                name: "IX_Sources_CoverPhotoId1",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "CoverPhotoId1",
                table: "Sources");

            migrationBuilder.CreateIndex(
                name: "IX_Sources_CoverPhotoId",
                table: "Sources",
                column: "CoverPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sources_Attachments_CoverPhotoId",
                table: "Sources",
                column: "CoverPhotoId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sources_Attachments_CoverPhotoId",
                table: "Sources");

            migrationBuilder.DropIndex(
                name: "IX_Sources_CoverPhotoId",
                table: "Sources");

            migrationBuilder.AddColumn<int>(
                name: "CoverPhotoId1",
                table: "Sources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sources_CoverPhotoId1",
                table: "Sources",
                column: "CoverPhotoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sources_Attachments_CoverPhotoId1",
                table: "Sources",
                column: "CoverPhotoId1",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
