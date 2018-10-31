using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class ChangeArticleFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorite_AspNetUsers_AppUserId",
                table: "ArticleFavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleFavorite",
                table: "ArticleFavorite");

            migrationBuilder.DropIndex(
                name: "IX_ArticleFavorite_AppUserId",
                table: "ArticleFavorite");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ArticleFavorite");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ArticleFavorite",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ArticleFavorite",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleFavorite",
                table: "ArticleFavorite",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFavorite_ArticleId",
                table: "ArticleFavorite",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFavorite_UserId",
                table: "ArticleFavorite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorite_AspNetUsers_UserId",
                table: "ArticleFavorite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleFavorite_AspNetUsers_UserId",
                table: "ArticleFavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleFavorite",
                table: "ArticleFavorite");

            migrationBuilder.DropIndex(
                name: "IX_ArticleFavorite_ArticleId",
                table: "ArticleFavorite");

            migrationBuilder.DropIndex(
                name: "IX_ArticleFavorite_UserId",
                table: "ArticleFavorite");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ArticleFavorite");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ArticleFavorite",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ArticleFavorite",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleFavorite",
                table: "ArticleFavorite",
                columns: new[] { "ArticleId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleFavorite_AppUserId",
                table: "ArticleFavorite",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleFavorite_AspNetUsers_AppUserId",
                table: "ArticleFavorite",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
