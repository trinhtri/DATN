using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_v12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Issues_Sprint_Id",
                table: "Issues",
                column: "Sprint_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Sprints_Sprint_Id",
                table: "Issues",
                column: "Sprint_Id",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Sprints_Sprint_Id",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Sprint_Id",
                table: "Issues");
        }
    }
}
