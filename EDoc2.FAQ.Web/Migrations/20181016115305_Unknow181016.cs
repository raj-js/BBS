using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class Unknow181016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentOp_ArticleComments_CommentId",
                table: "CommentOp");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentOp_AspNetUsers_OperatorId",
                table: "CommentOp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentOp",
                table: "CommentOp");

            migrationBuilder.RenameTable(
                name: "CommentOp",
                newName: "CommentOps");

            migrationBuilder.RenameIndex(
                name: "IX_CommentOp_OperatorId",
                table: "CommentOps",
                newName: "IX_CommentOps_OperatorId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentOp_CommentId",
                table: "CommentOps",
                newName: "IX_CommentOps_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentOps",
                table: "CommentOps",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentOps_ArticleComments_CommentId",
                table: "CommentOps",
                column: "CommentId",
                principalTable: "ArticleComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentOps_AspNetUsers_OperatorId",
                table: "CommentOps",
                column: "OperatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentOps_ArticleComments_CommentId",
                table: "CommentOps");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentOps_AspNetUsers_OperatorId",
                table: "CommentOps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentOps",
                table: "CommentOps");

            migrationBuilder.RenameTable(
                name: "CommentOps",
                newName: "CommentOp");

            migrationBuilder.RenameIndex(
                name: "IX_CommentOps_OperatorId",
                table: "CommentOp",
                newName: "IX_CommentOp_OperatorId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentOps_CommentId",
                table: "CommentOp",
                newName: "IX_CommentOp_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentOp",
                table: "CommentOp",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentOp_ArticleComments_CommentId",
                table: "CommentOp",
                column: "CommentId",
                principalTable: "ArticleComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentOp_AspNetUsers_OperatorId",
                table: "CommentOp",
                column: "OperatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
