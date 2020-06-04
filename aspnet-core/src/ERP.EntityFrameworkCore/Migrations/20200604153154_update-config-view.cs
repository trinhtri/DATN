using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class updateconfigview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIssueType",
                table: "ConfigView");

            migrationBuilder.DropColumn(
                name: "IsProject",
                table: "ConfigView");

            migrationBuilder.RenameColumn(
                name: "IsUpdateDate",
                table: "ConfigView",
                newName: "IsPriority");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPriority",
                table: "ConfigView",
                newName: "IsUpdateDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsIssueType",
                table: "ConfigView",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProject",
                table: "ConfigView",
                nullable: false,
                defaultValue: false);
        }
    }
}
