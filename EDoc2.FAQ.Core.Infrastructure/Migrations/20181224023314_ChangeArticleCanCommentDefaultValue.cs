using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    public partial class ChangeArticleCanCommentDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CanComment",
                table: "Article",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "CanComment",
                table: "Article",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool));
        }
    }
}
