using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_issue_v124 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Sprint_Id",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Sprint_Id",
                table: "Issues",
                newName: "Project_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Sprint_Id",
                table: "Issues",
                newName: "IX_Issues_Project_Id");

            migrationBuilder.AddColumn<long>(
                name: "Parent_Id",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Project_Id",
                table: "Issues",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Project_Id",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Parent_Id",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Project_Id",
                table: "Issues",
                newName: "Sprint_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Project_Id",
                table: "Issues",
                newName: "IX_Issues_Sprint_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Sprint_Id",
                table: "Issues",
                column: "Sprint_Id",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
