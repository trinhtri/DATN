using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Type_ID",
                table: "Tasks",
                newName: "Type_Id");

            migrationBuilder.RenameColumn(
                name: "Parent_Id",
                table: "Tasks",
                newName: "Sprint_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Sprint_Id",
                table: "Tasks",
                column: "Sprint_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sprints_Sprint_Id",
                table: "Tasks",
                column: "Sprint_Id",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sprints_Sprint_Id",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Sprint_Id",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Type_Id",
                table: "Tasks",
                newName: "Type_ID");

            migrationBuilder.RenameColumn(
                name: "Sprint_Id",
                table: "Tasks",
                newName: "Parent_Id");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }
    }
}
