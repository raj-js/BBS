using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCommentStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommentStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperationSourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperationSourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperatorId = table.Column<string>(maxLength: 50, nullable: false),
                    SourceId = table.Column<string>(maxLength: 50, nullable: false),
                    SourceTypeId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleOperations_ArticleOperationSourceTypes_SourceTypeId",
                        column: x => x.SourceTypeId,
                        principalTable: "ArticleOperationSourceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleOperations_ArticleOperationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ArticleOperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Summary = table.Column<string>(maxLength: 256, nullable: true),
                    Content = table.Column<string>(nullable: false),
                    Keywords = table.Column<string>(maxLength: 50, nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    CanComment = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatorId = table.Column<string>(maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleStates_StateId",
                        column: x => x.StateId,
                        principalTable: "ArticleStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ArticleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentCommentId = table.Column<long>(nullable: true),
                    ArticleId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    CreatorId = table.Column<string>(maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleComments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleComments_ArticleCommentStates_StateId",
                        column: x => x.StateId,
                        principalTable: "ArticleCommentStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleProperties_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_ArticleId",
                table: "ArticleComments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComments_StateId",
                table: "ArticleComments",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOperations_SourceTypeId",
                table: "ArticleOperations",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOperations_TypeId",
                table: "ArticleOperations",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleProperties_ArticleId",
                table: "ArticleProperties",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_StateId",
                table: "Articles",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TypeId",
                table: "Articles",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleComments");

            migrationBuilder.DropTable(
                name: "ArticleOperations");

            migrationBuilder.DropTable(
                name: "ArticleProperties");

            migrationBuilder.DropTable(
                name: "ArticleCommentStates");

            migrationBuilder.DropTable(
                name: "ArticleOperationSourceTypes");

            migrationBuilder.DropTable(
                name: "ArticleOperationTypes");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "ArticleStates");

            migrationBuilder.DropTable(
                name: "ArticleTypes");
        }
    }
}
