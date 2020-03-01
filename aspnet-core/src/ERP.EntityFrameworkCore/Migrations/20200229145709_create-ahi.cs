using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Migrations
{
    public partial class createahi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
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
                    ProjectCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProjectName = table.Column<string>(maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ClientName = table.Column<string>(maxLength: 100, nullable: true),
                    Discription = table.Column<string>(maxLength: 2000, nullable: true),
                    Note = table.Column<string>(maxLength: 2000, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
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
                    DocumentName = table.Column<string>(maxLength: 100, nullable: true),
                    Discription = table.Column<string>(maxLength: 2000, nullable: true),
                    Size = table.Column<decimal>(nullable: false),
                    Project_Id = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
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
                    IssueCode = table.Column<string>(maxLength: 100, nullable: true),
                    IssueName = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Discription = table.Column<string>(maxLength: 2000, nullable: true),
                    Project_Id = table.Column<int>(nullable: false),
                    Project_Id1 = table.Column<long>(nullable: true),
                    Assignee_Id = table.Column<long>(nullable: false),
                    Reporter_Id = table.Column<long>(nullable: false),
                    Due_Date = table.Column<DateTime>(nullable: true),
                    Update_Date = table.Column<DateTime>(nullable: true),
                    Resolved_Date = table.Column<DateTime>(nullable: true),
                    Estimate = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Projects_Project_Id1",
                        column: x => x.Project_Id1,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Members",
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
                    Role = table.Column<int>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Note = table.Column<string>(maxLength: 2000, nullable: true),
                    Project_Id = table.Column<long>(nullable: false),
                    Employee_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_AbpUsers_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Projects_Project_Id",
                        column: x => x.Project_Id,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
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
                    Discription = table.Column<string>(maxLength: 2000, nullable: true),
                    Employee_Id = table.Column<long>(nullable: false),
                    Project_Id = table.Column<int>(nullable: false),
                    Project_Id1 = table.Column<long>(nullable: true),
                    Issue_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AbpUsers_Employee_Id",
                        column: x => x.Employee_Id,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Issues_Issue_Id",
                        column: x => x.Issue_Id,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Projects_Project_Id1",
                        column: x => x.Project_Id1,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Employee_Id",
                table: "Comments",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Issue_Id",
                table: "Comments",
                column: "Issue_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Project_Id1",
                table: "Comments",
                column: "Project_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProjectId",
                table: "Documents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_Project_Id1",
                table: "Issues",
                column: "Project_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Employee_Id",
                table: "Members",
                column: "Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Project_Id",
                table: "Members",
                column: "Project_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
