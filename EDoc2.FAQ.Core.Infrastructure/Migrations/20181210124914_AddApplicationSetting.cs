using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    public partial class AddApplicationSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Version = table.Column<string>(maxLength: 50, nullable: false),
                    IconBase64 = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    ApplicationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSetting_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSetting_ApplicationId",
                table: "ApplicationSetting",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "Application");
        }
    }
}
