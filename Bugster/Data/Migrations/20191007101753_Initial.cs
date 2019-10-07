using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bugster.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Bound = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Priority = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    AssigneeId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    Role = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    ProjectId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    ManagerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Bound", "Name" },
                values: new object[,]
                {
                    { 1L, "FRONTEND", "JQuery" },
                    { 20L, "BACKEND", "Kafka" },
                    { 19L, "BACKEND", "OAuth" },
                    { 18L, "BACKEND", "Rabbit MQ" },
                    { 17L, "BACKEND", "MS SQL" },
                    { 16L, "BACKEND", "PostgreSQL" },
                    { 15L, "BACKEND", "MySQL" },
                    { 14L, "BACKEND", "Node.js" },
                    { 13L, "BACKEND", ".NET Core" },
                    { 12L, "BACKEND", "Java" },
                    { 11L, "BACKEND", "Php" },
                    { 10L, "FRONTEND", "React Native" },
                    { 9L, "FRONTEND", "Bootstrap" },
                    { 8L, "FRONTEND", "GraphQL" },
                    { 7L, "FRONTEND", "CSS" },
                    { 6L, "FRONTEND", "HTML" },
                    { 5L, "FRONTEND", "JavaScript" },
                    { 4L, "FRONTEND", "Vue.js" },
                    { 3L, "FRONTEND", "Angular" },
                    { 2L, "FRONTEND", "React" },
                    { 21L, "BACKEND", "Docker" },
                    { 22L, "BACKEND", "Mongo DB" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_AssigneeId",
                table: "Bugs",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                table: "Projects",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectId",
                table: "Users",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Users_AssigneeId",
                table: "Bugs",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bugs_Projects_ProjectId",
                table: "Bugs",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_ProjectId",
                table: "Users",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_ManagerId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
