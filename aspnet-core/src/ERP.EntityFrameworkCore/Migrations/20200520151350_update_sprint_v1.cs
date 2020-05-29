using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_sprint_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_Projects_Project_Id",
                table: "Sprints");

            migrationBuilder.AlterColumn<long>(
                name: "Project_Id",
                table: "Sprints",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_Projects_Project_Id",
                table: "Sprints",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_Projects_Project_Id",
                table: "Sprints");

            migrationBuilder.AlterColumn<long>(
                name: "Project_Id",
                table: "Sprints",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_Projects_Project_Id",
                table: "Sprints",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
