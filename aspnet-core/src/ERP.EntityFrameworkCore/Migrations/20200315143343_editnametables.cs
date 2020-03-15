using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class editnametables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Priority_Priority_Id",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueType_Type_ID",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Priority",
                table: "Priority");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueType",
                table: "IssueType");

            migrationBuilder.RenameTable(
                name: "Priority",
                newName: "Prioritys");

            migrationBuilder.RenameTable(
                name: "IssueType",
                newName: "IssueTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prioritys",
                table: "Prioritys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueTypes",
                table: "IssueTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Prioritys_Priority_Id",
                table: "Issues",
                column: "Priority_Id",
                principalTable: "Prioritys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_IssueTypes_Type_ID",
                table: "Issues",
                column: "Type_ID",
                principalTable: "IssueTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Prioritys_Priority_Id",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueTypes_Type_ID",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prioritys",
                table: "Prioritys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueTypes",
                table: "IssueTypes");

            migrationBuilder.RenameTable(
                name: "Prioritys",
                newName: "Priority");

            migrationBuilder.RenameTable(
                name: "IssueTypes",
                newName: "IssueType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Priority",
                table: "Priority",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueType",
                table: "IssueType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Priority_Priority_Id",
                table: "Issues",
                column: "Priority_Id",
                principalTable: "Priority",
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
    }
}
