using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class status_is_forgin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Status_Status_ID",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Status_ID",
                table: "Issues",
                newName: "Status_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Status_ID",
                table: "Issues",
                newName: "IX_Issues_Status_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Status_Status_Id",
                table: "Issues",
                column: "Status_Id",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Status_Status_Id",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Status_Id",
                table: "Issues",
                newName: "Status_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_Status_Id",
                table: "Issues",
                newName: "IX_Issues_Status_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Status_Status_ID",
                table: "Issues",
                column: "Status_ID",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
