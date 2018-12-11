using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    public partial class AddUserProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserProperty",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserProperty",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
