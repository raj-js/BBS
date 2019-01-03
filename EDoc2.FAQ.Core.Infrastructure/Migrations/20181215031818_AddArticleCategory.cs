using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    public partial class AddArticleCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryArticle");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Article",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryId",
                table: "Article",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Category_CategoryId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_CategoryId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Article");

            migrationBuilder.CreateTable(
                name: "CategoryArticle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryArticle_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryArticle_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryArticle_ArticleId",
                table: "CategoryArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryArticle_CategoryId",
                table: "CategoryArticle",
                column: "CategoryId");
        }
    }
}
