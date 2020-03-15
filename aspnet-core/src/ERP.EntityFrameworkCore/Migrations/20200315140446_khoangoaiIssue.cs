using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class khoangoaiIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Issues");

            migrationBuilder.AddColumn<long>(
                name: "Status_ID",
                table: "Issues",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Type_ID",
                table: "Issues",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Status_ID",
                table: "Issues",
                column: "Status_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Type_ID",
                table: "Issues",
                column: "Type_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Status_Status_ID",
                table: "Issues",
                column: "Status_ID",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueType_Type_ID",
                table: "Issues",
                column: "Type_ID",
                principalTable: "IssueType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Status_Status_ID",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueType_Type_ID",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Status_ID",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Type_ID",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Status_ID",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Type_ID",
                table: "Issues");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Issues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Issues",
                nullable: false,
                defaultValue: 0);
        }
    }
}
