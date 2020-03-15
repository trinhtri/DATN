using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class updateissue1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Project_Id1",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Project_Id1",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Project_Id1",
                table: "Issues");

            migrationBuilder.AlterColumn<long>(
                name: "Project_Id",
                table: "Issues",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Project_Id",
                table: "Issues",
                column: "Project_Id");

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

            migrationBuilder.DropIndex(
                name: "IX_Issues_Project_Id",
                table: "Issues");

            migrationBuilder.AlterColumn<int>(
                name: "Project_Id",
                table: "Issues",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "Project_Id1",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Project_Id1",
                table: "Issues",
                column: "Project_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Project_Id1",
                table: "Issues",
                column: "Project_Id1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
