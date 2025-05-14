using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSourceDateConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceDates_DateFromats_DateFormatId",
                table: "SourceDates");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceDates_DateFromats_DateFormatId",
                table: "SourceDates",
                column: "DateFormatId",
                principalTable: "DateFromats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SourceDates_DateFromats_DateFormatId",
                table: "SourceDates");

            migrationBuilder.AddForeignKey(
                name: "FK_SourceDates_DateFromats_DateFormatId",
                table: "SourceDates",
                column: "DateFormatId",
                principalTable: "DateFromats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
