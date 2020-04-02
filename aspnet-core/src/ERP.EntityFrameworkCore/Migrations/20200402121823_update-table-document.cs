using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class updatetabledocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Documents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Size",
                table: "Documents",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
