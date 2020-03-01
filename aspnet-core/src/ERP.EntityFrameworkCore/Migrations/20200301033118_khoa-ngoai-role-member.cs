using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class khoangoairolemember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Members");

            migrationBuilder.AddColumn<long>(
                name: "Role_id",
                table: "Members",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Role_id",
                table: "Members",
                column: "Role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_RoleProject_Role_id",
                table: "Members",
                column: "Role_id",
                principalTable: "RoleProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_RoleProject_Role_id",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_Role_id",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Role_id",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Members",
                nullable: false,
                defaultValue: 0);
        }
    }
}
