using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class ChangeCommentOpKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentOps",
                table: "CommentOps");

            migrationBuilder.DropIndex(
                name: "IX_CommentOps_CommentId",
                table: "CommentOps");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CommentOps");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorId",
                table: "CommentOps",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentOps",
                table: "CommentOps",
                columns: new[] { "CommentId", "OperatorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentOps",
                table: "CommentOps");

            migrationBuilder.AlterColumn<string>(
                name: "OperatorId",
                table: "CommentOps",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CommentOps",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentOps",
                table: "CommentOps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOps_CommentId",
                table: "CommentOps",
                column: "CommentId");
        }
    }
}
