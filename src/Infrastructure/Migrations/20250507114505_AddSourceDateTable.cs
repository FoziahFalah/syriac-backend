using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSourceDateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "SourceDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<int>(type: "int", nullable: false),
                    DateFormatId = table.Column<int>(type: "int", nullable: false),
                    FromYear = table.Column<int>(type: "int", nullable: false),
                    ToYear = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceDates_DateFromats_DateFormatId",
                        column: x => x.DateFormatId,
                        principalTable: "DateFromats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SourceDates_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SourceDates_DateFormatId",
                table: "SourceDates",
                column: "DateFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceDates_SourceId",
                table: "SourceDates",
                column: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SourceDates");

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
        }
    }
}
