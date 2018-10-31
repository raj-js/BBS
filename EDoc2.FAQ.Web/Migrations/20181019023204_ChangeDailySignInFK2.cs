using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class ChangeDailySignInFK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailySignIns_AspNetUsers_AppUserId",
                table: "DailySignIns");

            migrationBuilder.DropForeignKey(
                name: "FK_LogScores_AspNetUsers_AppUserId",
                table: "LogScores");

            migrationBuilder.DropIndex(
                name: "IX_LogScores_AppUserId",
                table: "LogScores");

            migrationBuilder.DropIndex(
                name: "IX_DailySignIns_AppUserId",
                table: "DailySignIns");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "LogScores");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "DailySignIns");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "LogScores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "DailySignIns",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogScores_AppUserId",
                table: "LogScores",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySignIns_AppUserId",
                table: "DailySignIns",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailySignIns_AspNetUsers_AppUserId",
                table: "DailySignIns",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LogScores_AspNetUsers_AppUserId",
                table: "LogScores",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
