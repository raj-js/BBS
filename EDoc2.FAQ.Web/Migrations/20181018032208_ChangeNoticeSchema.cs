using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Web.Migrations
{
    public partial class ChangeNoticeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notices_AspNetUsers_TagetUserId",
                table: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_Notices_TagetUserId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "TargetType",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notices");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "Notices",
                newName: "WhoId");

            migrationBuilder.RenameColumn(
                name: "TagetUserId",
                table: "Notices",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "OperateDate",
                table: "Notices",
                newName: "When");

            migrationBuilder.AddColumn<int>(
                name: "ReportTargetType",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Notices",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "What",
                table: "Notices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Where",
                table: "Notices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Who",
                table: "Notices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NoticeReceive",
                columns: table => new
                {
                    NoticeId = table.Column<string>(nullable: false),
                    ReveiverId = table.Column<string>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    ReadDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeReceive", x => new { x.NoticeId, x.ReveiverId });
                    table.ForeignKey(
                        name: "FK_NoticeReceive_Notices_NoticeId",
                        column: x => x.NoticeId,
                        principalTable: "Notices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoticeReceive_AspNetUsers_ReveiverId",
                        column: x => x.ReveiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoticeReceive_ReveiverId",
                table: "NoticeReceive",
                column: "ReveiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoticeReceive");

            migrationBuilder.DropColumn(
                name: "ReportTargetType",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "What",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "Where",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "Who",
                table: "Notices");

            migrationBuilder.RenameColumn(
                name: "WhoId",
                table: "Notices",
                newName: "TargetId");

            migrationBuilder.RenameColumn(
                name: "When",
                table: "Notices",
                newName: "OperateDate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Notices",
                newName: "TagetUserId");

            migrationBuilder.AddColumn<int>(
                name: "TargetType",
                table: "Reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TagetUserId",
                table: "Notices",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Notices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Notices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notices_TagetUserId",
                table: "Notices",
                column: "TagetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_AspNetUsers_TagetUserId",
                table: "Notices",
                column: "TagetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
