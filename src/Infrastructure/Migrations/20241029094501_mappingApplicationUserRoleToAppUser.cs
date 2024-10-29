using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mappingApplicationUserRoleToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserRoles_Contributors_ContributorId",
                table: "ApplicationUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserRoles_ContributorId",
                table: "ApplicationUserRoles");

            migrationBuilder.RenameColumn(
                name: "ContributorId",
                table: "ApplicationUserRoles",
                newName: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserRoles",
                newName: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_ContributorId",
                table: "ApplicationUserRoles",
                column: "ContributorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserRoles_Contributors_ContributorId",
                table: "ApplicationUserRoles",
                column: "ContributorId",
                principalTable: "Contributors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
