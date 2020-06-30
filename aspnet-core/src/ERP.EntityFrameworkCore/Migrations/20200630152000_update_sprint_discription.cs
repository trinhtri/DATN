using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_sprint_discription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SprintName",
                table: "Sprints",
                newName: "SprintCode");

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "Tasks",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discription",
                table: "Sprints",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discription",
                table: "Sprints");

            migrationBuilder.RenameColumn(
                name: "SprintCode",
                table: "Sprints",
                newName: "SprintName");

            migrationBuilder.AlterColumn<string>(
                name: "Discription",
                table: "Tasks",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
