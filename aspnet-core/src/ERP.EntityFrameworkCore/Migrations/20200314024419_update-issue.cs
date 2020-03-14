using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class updateissue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssueName",
                table: "Issues",
                newName: "Summary");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Issues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Issues",
                newName: "IssueName");
        }
    }
}
