using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class resolve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Resolve_Id",
                table: "Issues",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Resolve",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    ResolveName = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolve", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Resolve_Id",
                table: "Issues",
                column: "Resolve_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Resolve_Resolve_Id",
                table: "Issues",
                column: "Resolve_Id",
                principalTable: "Resolve",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Resolve_Resolve_Id",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Resolve");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Resolve_Id",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Resolve_Id",
                table: "Issues");
        }
    }
}
