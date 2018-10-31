using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class CreateArticleSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleSpeCol",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Display = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleSpeCol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PublisherId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    SpeColId = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    IsTop = table.Column<bool>(nullable: false),
                    TopDate = table.Column<DateTime>(nullable: true),
                    IsTopTimeout = table.Column<bool>(nullable: false),
                    IsCream = table.Column<bool>(nullable: false),
                    CreamDate = table.Column<DateTime>(nullable: true),
                    IsCreamTimeout = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_AspNetUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_ArticleSpeCol_SpeColId",
                        column: x => x.SpeColId,
                        principalTable: "ArticleSpeCol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComment",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IsReplyToUser = table.Column<bool>(nullable: false),
                    ArticleId = table.Column<string>(nullable: false),
                    FromUserId = table.Column<string>(nullable: true),
                    ToUserId = table.Column<string>(nullable: true),
                    ReplyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleComment_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleComment_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleComment_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_PublisherId",
                table: "Article",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_SpeColId",
                table: "Article",
                column: "SpeColId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ArticleId",
                table: "ArticleComment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_FromUserId",
                table: "ArticleComment",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ToUserId",
                table: "ArticleComment",
                column: "ToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleComment");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "ArticleSpeCol");
        }
    }
}
