using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCoverPhotoIdFromSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sources_ApplicationUsers_IntroductionEditorId",
                table: "Sources");

            migrationBuilder.DropForeignKey(
                name: "FK_Sources_Attachments_CoverPhotoId",
                table: "Sources");

            migrationBuilder.DropIndex(
                name: "IX_Sources_CoverPhotoId",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "CoverPhotoId",
                table: "Sources");

            migrationBuilder.AlterColumn<int>(
                name: "IntroductionEditorId",
                table: "Sources",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CoverPhotos_SourceId",
                table: "CoverPhotos",
                column: "SourceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_Sources_SourceId",
                table: "CoverPhotos",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sources_ApplicationUsers_IntroductionEditorId",
                table: "Sources",
                column: "IntroductionEditorId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_Sources_SourceId",
                table: "CoverPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Sources_ApplicationUsers_IntroductionEditorId",
                table: "Sources");

            migrationBuilder.DropIndex(
                name: "IX_CoverPhotos_SourceId",
                table: "CoverPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "IntroductionEditorId",
                table: "Sources",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoverPhotoId",
                table: "Sources",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sources_CoverPhotoId",
                table: "Sources",
                column: "CoverPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sources_ApplicationUsers_IntroductionEditorId",
                table: "Sources",
                column: "IntroductionEditorId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sources_Attachments_CoverPhotoId",
                table: "Sources",
                column: "CoverPhotoId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
