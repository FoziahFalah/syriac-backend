using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserExcerptRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_EditorId",
                table: "ExcerptTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_ReviewerId",
                table: "ExcerptTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_TranslatorId",
                table: "ExcerptTexts");

            migrationBuilder.AddColumn<int>(
                name: "ExcerptId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExcerptTexts_ExcerptId",
                table: "ExcerptTexts",
                column: "ExcerptId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcerptDates_ExcerptId",
                table: "ExcerptDates",
                column: "ExcerptId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ExcerptId",
                table: "Comments",
                column: "ExcerptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Excerpts_ExcerptId",
                table: "Comments",
                column: "ExcerptId",
                principalTable: "Excerpts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptDates_Excerpts_ExcerptId",
                table: "ExcerptDates",
                column: "ExcerptId",
                principalTable: "Excerpts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_EditorId",
                table: "ExcerptTexts",
                column: "EditorId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_ReviewerId",
                table: "ExcerptTexts",
                column: "ReviewerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_TranslatorId",
                table: "ExcerptTexts",
                column: "TranslatorId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_Excerpts_ExcerptId",
                table: "ExcerptTexts",
                column: "ExcerptId",
                principalTable: "Excerpts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Excerpts_ExcerptId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptDates_Excerpts_ExcerptId",
                table: "ExcerptDates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_EditorId",
                table: "ExcerptTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_ReviewerId",
                table: "ExcerptTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_TranslatorId",
                table: "ExcerptTexts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerptTexts_Excerpts_ExcerptId",
                table: "ExcerptTexts");

            migrationBuilder.DropIndex(
                name: "IX_ExcerptTexts_ExcerptId",
                table: "ExcerptTexts");

            migrationBuilder.DropIndex(
                name: "IX_ExcerptDates_ExcerptId",
                table: "ExcerptDates");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ExcerptId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ExcerptId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_EditorId",
                table: "ExcerptTexts",
                column: "EditorId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_ReviewerId",
                table: "ExcerptTexts",
                column: "ReviewerId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerptTexts_ApplicationUsers_TranslatorId",
                table: "ExcerptTexts",
                column: "TranslatorId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
