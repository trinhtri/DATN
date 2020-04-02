using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class updatetablecomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_Project_Id1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Project_Id1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Project_Id",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Project_Id1",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Project_Id",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Project_Id1",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Project_Id1",
                table: "Comments",
                column: "Project_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_Project_Id1",
                table: "Comments",
                column: "Project_Id1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
