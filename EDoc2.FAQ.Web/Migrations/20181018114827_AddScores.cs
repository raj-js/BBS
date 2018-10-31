using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class AddScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Notices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailySignIns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySignIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailySignIns_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DailySignIns_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogScores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogScores_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LogScores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notices_SenderId",
                table: "Notices",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySignIns_AppUserId",
                table: "DailySignIns",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySignIns_UserId",
                table: "DailySignIns",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LogScores_AppUserId",
                table: "LogScores",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LogScores_UserId",
                table: "LogScores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_AspNetUsers_SenderId",
                table: "Notices",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notices_AspNetUsers_SenderId",
                table: "Notices");

            migrationBuilder.DropTable(
                name: "DailySignIns");

            migrationBuilder.DropTable(
                name: "LogScores");

            migrationBuilder.DropIndex(
                name: "IX_Notices_SenderId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Notices");
        }
    }
}
