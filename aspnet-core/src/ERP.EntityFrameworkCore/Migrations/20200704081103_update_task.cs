using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Assignee_Id",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Assignee_Id",
                table: "Tasks",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
