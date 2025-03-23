using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class normalizingRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationRolePermissions_ApplicationRoleId",
                table: "ApplicationRolePermissions");

            migrationBuilder.DropColumn(
                name: "UserRoles",
                table: "ApplicationUserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationPermissionIds",
                table: "ApplicationRolePermissions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ApplicationUserRoles",
                newName: "ApplicationUserId");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationRoleId",
                table: "ApplicationUserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationRoleId",
                table: "ApplicationUserRoles",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ApplicationUserId",
                table: "ApplicationUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                column: "ApplicationPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_ApplicationRoleId_ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                columns: new[] { "ApplicationRoleId", "ApplicationPermissionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationRolePermissions_ApplicationPermissions_ApplicationPermissionId",
                table: "ApplicationRolePermissions",
                column: "ApplicationPermissionId",
                principalTable: "ApplicationPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationRoles_ApplicationRoleId",
                table: "ApplicationUserRoles",
                column: "ApplicationRoleId",
                principalTable: "ApplicationRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationUsers_ApplicationUserId",
                table: "ApplicationUserRoles",
                column: "ApplicationUserId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationRolePermissions_ApplicationPermissions_ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationRoles_ApplicationRoleId",
                table: "ApplicationUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRoles_ApplicationUsers_ApplicationUserId",
                table: "ApplicationUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRoles_ApplicationRoleId",
                table: "ApplicationUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRoles_ApplicationUserId",
                table: "ApplicationUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationRolePermissions_ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationRolePermissions_ApplicationRoleId_ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "ApplicationUserRoles");

            migrationBuilder.DropColumn(
                name: "ApplicationPermissionId",
                table: "ApplicationRolePermissions");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserRoles",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserRoles",
                table: "ApplicationUserRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationPermissionIds",
                table: "ApplicationRolePermissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermissions_ApplicationRoleId",
                table: "ApplicationRolePermissions",
                column: "ApplicationRoleId");
        }
    }
}
