using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class RemoveSpecAndLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleSpeCols_ArticleSpeColId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleLabels");

            migrationBuilder.DropTable(
                name: "ArticleSpeCols");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ArticleSpeColId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleSpeColId",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleSpeColId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleSpeCols",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Display = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleSpeCols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Display = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleLabels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ArticleId = table.Column<string>(nullable: false),
                    LabelId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleLabels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleLabels_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleLabels_Labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleSpeColId",
                table: "Articles",
                column: "ArticleSpeColId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLabels_ArticleId",
                table: "ArticleLabels",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleLabels_LabelId",
                table: "ArticleLabels",
                column: "LabelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleSpeCols_ArticleSpeColId",
                table: "Articles",
                column: "ArticleSpeColId",
                principalTable: "ArticleSpeCols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
