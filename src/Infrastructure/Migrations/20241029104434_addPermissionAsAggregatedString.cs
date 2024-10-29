using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addPermissionAsAggregatedString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRolePermissions_ApplicationPermission_ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationRolePermissions_ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationPermissionIds",
                table: "ApplicationRolePermissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationPermissionIds",
                table: "ApplicationRolePermissions");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                column: "ApplicationPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRolePermissions_ApplicationPermission_ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                column: "ApplicationPermissionId",
                principalTable: "ApplicationPermission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
