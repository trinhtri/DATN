using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_issue_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Issues_Issue_Id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Projects_Project_Id",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issues",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Project_Id",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Project_Id",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Resolved_Date",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Update_Date",
                table: "Issues");

            migrationBuilder.RenameTable(
                name: "Issues",
                newName: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IssueCode",
                table: "Tasks",
                newName: "TaskCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Tasks_Issue_Id",
                table: "Comments",
                column: "Issue_Id",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Tasks_Issue_Id",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Issues");

            migrationBuilder.RenameColumn(
                name: "TaskCode",
                table: "Issues",
                newName: "IssueCode");

            migrationBuilder.AddColumn<long>(
                name: "Project_Id",
                table: "Issues",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Resolved_Date",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Update_Date",
                table: "Issues",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issues",
                table: "Issues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Project_Id",
                table: "Issues",
                column: "Project_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Issues_Issue_Id",
                table: "Comments",
                column: "Issue_Id",
                principalTable: "Issues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Projects_Project_Id",
                table: "Issues",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
