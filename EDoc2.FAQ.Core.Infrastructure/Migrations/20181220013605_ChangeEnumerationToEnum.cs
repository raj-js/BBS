using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    public partial class ChangeEnumerationToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserScoreHistory_UserScoreChangeReason_ReasonId",
                table: "UserScoreHistory");

            migrationBuilder.DropTable(
                name: "UserScoreChangeReason");

            migrationBuilder.DropIndex(
                name: "IX_UserScoreHistory_ReasonId",
                table: "UserScoreHistory");

            migrationBuilder.DropColumn(
                name: "ReasonId",
                table: "UserScoreHistory");

            migrationBuilder.AddColumn<int>(
                name: "Reason",
                table: "UserScoreHistory",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "UserScoreHistory");

            migrationBuilder.AddColumn<int>(
                name: "ReasonId",
                table: "UserScoreHistory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserScoreChangeReason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScoreChangeReason", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserScoreHistory_ReasonId",
                table: "UserScoreHistory",
                column: "ReasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserScoreHistory_UserScoreChangeReason_ReasonId",
                table: "UserScoreHistory",
                column: "ReasonId",
                principalTable: "UserScoreChangeReason",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
