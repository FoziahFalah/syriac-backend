using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateRangeAndIdsToSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_Sources_SourceId",
                table: "CoverPhotos");

            migrationBuilder.DropColumn(
                name: "DocumentedOnGregorian",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "DocumentedOnHijri",
                table: "Sources");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentedOnGregorianFrom",
                table: "Sources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentedOnGregorianTo",
                table: "Sources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentedOnHijriFrom",
                table: "Sources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentedOnHijriTo",
                table: "Sources",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBeforeGregorianEra",
                table: "Sources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBeforeHijriEra",
                table: "Sources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_Sources_SourceId",
                table: "CoverPhotos",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoverPhotos_Sources_SourceId",
                table: "CoverPhotos");

            migrationBuilder.DropColumn(
                name: "DocumentedOnGregorianFrom",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "DocumentedOnGregorianTo",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "DocumentedOnHijriFrom",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "DocumentedOnHijriTo",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "IsBeforeGregorianEra",
                table: "Sources");

            migrationBuilder.DropColumn(
                name: "IsBeforeHijriEra",
                table: "Sources");

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentedOnGregorian",
                table: "Sources",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentedOnHijri",
                table: "Sources",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_CoverPhotos_Sources_SourceId",
                table: "CoverPhotos",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
