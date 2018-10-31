using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class ChangeArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_AppUserId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_PublisherId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_ArticleSpeCol_SpeColId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComment_Article_ArticleId",
                table: "ArticleComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComment_AspNetUsers_FromUserId",
                table: "ArticleComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComment_AspNetUsers_ToUserId",
                table: "ArticleComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleSpeCol",
                table: "ArticleSpeCol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleComment",
                table: "ArticleComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.RenameTable(
                name: "ArticleSpeCol",
                newName: "ArticleSpeCols");

            migrationBuilder.RenameTable(
                name: "ArticleComment",
                newName: "ArticleComments");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "Articles");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComment_ToUserId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComment_FromUserId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComment_ArticleId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_SpeColId",
                table: "Articles",
                newName: "IX_Articles_SpeColId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_PublisherId",
                table: "Articles",
                newName: "IX_Articles_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_AppUserId",
                table: "Articles",
                newName: "IX_Articles_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleSpeCols",
                table: "ArticleSpeCols",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleComments",
                table: "ArticleComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Articles",
                table: "Articles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_Articles_ArticleId",
                table: "ArticleComments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_FromUserId",
                table: "ArticleComments",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_ToUserId",
                table: "ArticleComments",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_PublisherId",
                table: "Articles",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleSpeCols_SpeColId",
                table: "Articles",
                column: "SpeColId",
                principalTable: "ArticleSpeCols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_Articles_ArticleId",
                table: "ArticleComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_FromUserId",
                table: "ArticleComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_ToUserId",
                table: "ArticleComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_PublisherId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleSpeCols_SpeColId",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleSpeCols",
                table: "ArticleSpeCols");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Articles",
                table: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleComments",
                table: "ArticleComments");

            migrationBuilder.RenameTable(
                name: "ArticleSpeCols",
                newName: "ArticleSpeCol");

            migrationBuilder.RenameTable(
                name: "Articles",
                newName: "Article");

            migrationBuilder.RenameTable(
                name: "ArticleComments",
                newName: "ArticleComment");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_SpeColId",
                table: "Article",
                newName: "IX_Article_SpeColId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_PublisherId",
                table: "Article",
                newName: "IX_Article_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_AppUserId",
                table: "Article",
                newName: "IX_Article_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_ToUserId",
                table: "ArticleComment",
                newName: "IX_ArticleComment_ToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_FromUserId",
                table: "ArticleComment",
                newName: "IX_ArticleComment_FromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_ArticleId",
                table: "ArticleComment",
                newName: "IX_ArticleComment_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleSpeCol",
                table: "ArticleSpeCol",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleComment",
                table: "ArticleComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_AppUserId",
                table: "Article",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_PublisherId",
                table: "Article",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Article_ArticleSpeCol_SpeColId",
                table: "Article",
                column: "SpeColId",
                principalTable: "ArticleSpeCol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComment_Article_ArticleId",
                table: "ArticleComment",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComment_AspNetUsers_FromUserId",
                table: "ArticleComment",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComment_AspNetUsers_ToUserId",
                table: "ArticleComment",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
