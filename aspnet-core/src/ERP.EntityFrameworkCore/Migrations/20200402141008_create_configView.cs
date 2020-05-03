using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class create_configView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigView",
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
                    IsProject = table.Column<bool>(nullable: false),
                    IsIssue = table.Column<bool>(nullable: false),
                    IsSummary = table.Column<bool>(nullable: false),
                    IsIssueType = table.Column<bool>(nullable: false),
                    IsStatus = table.Column<bool>(nullable: false),
                    IsEstimate = table.Column<bool>(nullable: false),
                    IsReporter = table.Column<bool>(nullable: false),
                    IsAssignee = table.Column<bool>(nullable: false),
                    IsDueDate = table.Column<bool>(nullable: false),
                    IsCreatedDate = table.Column<bool>(nullable: false),
                    IsUpdateDate = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigView_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigView_UserId",
                table: "ConfigView",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigView");
        }
    }
}
