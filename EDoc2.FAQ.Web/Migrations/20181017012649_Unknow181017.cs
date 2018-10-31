using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class Unknow181017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentOps_AspNetUsers_OperatorId",
                table: "CommentOps");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorId",
                table: "CommentOps",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_CommentOps_AspNetUsers_OperatorId",
                table: "CommentOps",
                column: "OperatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentOps_AspNetUsers_OperatorId",
                table: "CommentOps");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorId",
                table: "CommentOps",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentOps_AspNetUsers_OperatorId",
                table: "CommentOps",
                column: "OperatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
