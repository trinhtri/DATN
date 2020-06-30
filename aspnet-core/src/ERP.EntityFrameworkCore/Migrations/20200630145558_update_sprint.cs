using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_sprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Tasks",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Assignee_Id",
                table: "Sprints",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "Due_Date",
                table: "Sprints",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Estimate",
                table: "Sprints",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Reporter_Id",
                table: "Sprints",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Sprints",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Status_Id",
                table: "Sprints",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Assignee_Id",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Due_Date",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Reporter_Id",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "Status_Id",
                table: "Sprints");
        }
    }
}
