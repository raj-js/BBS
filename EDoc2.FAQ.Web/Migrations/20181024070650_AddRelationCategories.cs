using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class AddRelationCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Articles_ArticleId",
                table: "ArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategory_Categories_CategoryId",
                table: "ArticleCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleSpeCols_SpeColId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_SpeColId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCategory",
                table: "ArticleCategory");

            migrationBuilder.DropColumn(
                name: "SpeColId",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "ArticleCategory",
                newName: "ArticleCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategory_CategoryId",
                table: "ArticleCategories",
                newName: "IX_ArticleCategories_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategory_ArticleId",
                table: "ArticleCategories",
                newName: "IX_ArticleCategories_ArticleId");

            migrationBuilder.AddColumn<string>(
                name: "ArticleSpeColId",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCategories",
                table: "ArticleCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleSpeColId",
                table: "Articles",
                column: "ArticleSpeColId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategories_Articles_ArticleId",
                table: "ArticleCategories",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategories_Categories_CategoryId",
                table: "ArticleCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleSpeCols_ArticleSpeColId",
                table: "Articles",
                column: "ArticleSpeColId",
                principalTable: "ArticleSpeCols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategories_Articles_ArticleId",
                table: "ArticleCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleCategories_Categories_CategoryId",
                table: "ArticleCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleSpeCols_ArticleSpeColId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleSpeColId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleCategories",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "ArticleSpeColId",
                table: "Articles");

            migrationBuilder.RenameTable(
                name: "ArticleCategories",
                newName: "ArticleCategory");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategories_CategoryId",
                table: "ArticleCategory",
                newName: "IX_ArticleCategory_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleCategories_ArticleId",
                table: "ArticleCategory",
                newName: "IX_ArticleCategory_ArticleId");

            migrationBuilder.AddColumn<string>(
                name: "SpeColId",
                table: "Articles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleCategory",
                table: "ArticleCategory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_SpeColId",
                table: "Articles",
                column: "SpeColId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Articles_ArticleId",
                table: "ArticleCategory",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleCategory_Categories_CategoryId",
                table: "ArticleCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleSpeCols_SpeColId",
                table: "Articles",
                column: "SpeColId",
                principalTable: "ArticleSpeCols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
