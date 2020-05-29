using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_member_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_RoleProject_Role_id",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_Role_id",
                table: "Members");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
