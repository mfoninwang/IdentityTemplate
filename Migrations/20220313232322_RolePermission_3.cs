using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class RolePermission_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Permission_PermissionId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_PermissionId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Role",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_PermissionId",
                table: "Role",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Permission_PermissionId",
                table: "Role",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "Id");
        }
    }
}
