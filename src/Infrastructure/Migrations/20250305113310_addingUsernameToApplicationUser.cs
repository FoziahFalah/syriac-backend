using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingUsernameToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityApplicationUserId",
                table: "ApplicationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityApplicationUserId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ApplicationUsers");
        }
    }
}
