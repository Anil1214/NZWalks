using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks_API.Migrations
{
    /// <inheritdoc />
    public partial class modifiedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_Role_RoleId",
                table: "User_Role");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Role_User_UserId",
                table: "User_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Role",
                table: "User_Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "User_Role",
                newName: "Users_Roles");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_UserId",
                table: "Users_Roles",
                newName: "IX_Users_Roles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Role_RoleId",
                table: "Users_Roles",
                newName: "IX_Users_Roles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Roles",
                table: "Users_Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Roles_RoleId",
                table: "Users_Roles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Users_UserId",
                table: "Users_Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Roles_RoleId",
                table: "Users_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Users_UserId",
                table: "Users_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Roles",
                table: "Users_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Users_Roles",
                newName: "User_Role");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Roles_UserId",
                table: "User_Role",
                newName: "IX_User_Role_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Roles_RoleId",
                table: "User_Role",
                newName: "IX_User_Role_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Role",
                table: "User_Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_Role_RoleId",
                table: "User_Role",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Role_User_UserId",
                table: "User_Role",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
