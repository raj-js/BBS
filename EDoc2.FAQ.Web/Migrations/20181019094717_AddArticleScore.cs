using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class AddArticleScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorite_Articles_ArticleId",
                table: "ArticleFavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorite_AspNetUsers_UserId",
                table: "ArticleFavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleFavorite",
                table: "ArticleFavorite");

            migrationBuilder.RenameTable(
                name: "ArticleFavorite",
                newName: "ArticleFavorites");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleFavorite_UserId",
                table: "ArticleFavorites",
                newName: "IX_ArticleFavorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleFavorite_ArticleId",
                table: "ArticleFavorites",
                newName: "IX_ArticleFavorites_ArticleId");

            migrationBuilder.AddColumn<int>(
                name: "RewardScore",
                table: "Articles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ArticleFavorites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleFavorites",
                table: "ArticleFavorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorites_Articles_ArticleId",
                table: "ArticleFavorites",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorites_AspNetUsers_UserId",
                table: "ArticleFavorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorites_Articles_ArticleId",
                table: "ArticleFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorites_AspNetUsers_UserId",
                table: "ArticleFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleFavorites",
                table: "ArticleFavorites");

            migrationBuilder.DropColumn(
                name: "RewardScore",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ArticleFavorites");

            migrationBuilder.RenameTable(
                name: "ArticleFavorites",
                newName: "ArticleFavorite");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleFavorites_UserId",
                table: "ArticleFavorite",
                newName: "IX_ArticleFavorite_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleFavorites_ArticleId",
                table: "ArticleFavorite",
                newName: "IX_ArticleFavorite_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleFavorite",
                table: "ArticleFavorite",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorite_Articles_ArticleId",
                table: "ArticleFavorite",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorite_AspNetUsers_UserId",
                table: "ArticleFavorite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
