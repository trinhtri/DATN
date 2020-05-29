using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class update_v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Prioritys_Priority_Id",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Resolve_Resolve_Id",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Status_Status_Id",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_IssueTypes_Type_ID",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "Resolve");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Priority_Id",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Resolve_Id",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Status_Id",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_Type_ID",
                table: "Issues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resolve",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ResolveName = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolve", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Priority_Id",
                table: "Issues",
                column: "Priority_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Resolve_Id",
                table: "Issues",
                column: "Resolve_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Status_Id",
                table: "Issues",
                column: "Status_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Type_ID",
                table: "Issues",
                column: "Type_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Prioritys_Priority_Id",
                table: "Issues",
                column: "Priority_Id",
                principalTable: "Prioritys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Resolve_Resolve_Id",
                table: "Issues",
                column: "Resolve_Id",
                principalTable: "Resolve",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Status_Status_Id",
                table: "Issues",
                column: "Status_Id",
                principalTable: "Status",
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
    }
}
