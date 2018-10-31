using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class ChangeArtcleReplyTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_AspNetUsers_ToUserId",
                table: "ArticleComments");

            migrationBuilder.RenameColumn(
                name: "ToUserId",
                table: "ArticleComments",
                newName: "ReplyCommentId");

            migrationBuilder.RenameColumn(
                name: "IsReplyToUser",
                table: "ArticleComments",
                newName: "IsReplyToComment");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_ToUserId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_ReplyCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_ArticleComments_ReplyCommentId",
                table: "ArticleComments",
                column: "ReplyCommentId",
                principalTable: "ArticleComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleComments_ArticleComments_ReplyCommentId",
                table: "ArticleComments");

            migrationBuilder.RenameColumn(
                name: "ReplyCommentId",
                table: "ArticleComments",
                newName: "ToUserId");

            migrationBuilder.RenameColumn(
                name: "IsReplyToComment",
                table: "ArticleComments",
                newName: "IsReplyToUser");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleComments_ReplyCommentId",
                table: "ArticleComments",
                newName: "IX_ArticleComments_ToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleComments_AspNetUsers_ToUserId",
                table: "ArticleComments",
                column: "ToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
