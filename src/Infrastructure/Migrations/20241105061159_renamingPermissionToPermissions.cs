using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class renamingPermissionToPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationPermission",
                table: "ApplicationPermission");

            migrationBuilder.RenameTable(
                name: "ApplicationPermission",
                newName: "ApplicationPermissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationPermissions",
                table: "ApplicationPermissions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationPermissions",
                table: "ApplicationPermissions");

            migrationBuilder.RenameTable(
                name: "ApplicationPermissions",
                newName: "ApplicationPermission");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationPermission",
                table: "ApplicationPermission",
                column: "Id");
        }
    }
}
