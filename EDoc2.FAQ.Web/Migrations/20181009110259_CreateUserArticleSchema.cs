using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class CreateUserArticleSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Article",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_AppUserId",
                table: "Article",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_AppUserId",
                table: "Article",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_AppUserId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_AppUserId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Article");
        }
    }
}
