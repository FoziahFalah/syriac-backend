using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyriacSources.Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingPolicyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.RenameColumn(
                name: "NormalizedPermissionName",
                table: "ApplicationPermission",
                newName: "PolicyName");

            migrationBuilder.RenameIndex(
                name: "NormalizedPermissionName",
                table: "ApplicationPermission",
                newName: "PolicyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PolicyName",
                table: "ApplicationPermission",
                newName: "NormalizedPermissionName");

            migrationBuilder.RenameIndex(
                name: "PolicyName",
                table: "ApplicationPermission",
                newName: "NormalizedPermissionName");
        }
    }
}
