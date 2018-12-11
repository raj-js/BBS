using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCommentState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCommentState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperationOperatorType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperationOperatorType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperationTargetType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperationTargetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperationType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsMuted = table.Column<bool>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 50, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 128, nullable: true),
                    Signature = table.Column<string>(maxLength: 256, nullable: true),
                    JoinDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleOperation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Operator = table.Column<string>(maxLength: 50, nullable: false),
                    OperatorTypeId = table.Column<int>(nullable: true),
                    Target = table.Column<string>(maxLength: 50, nullable: false),
                    SourceTypeId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleOperation_ArticleOperationOperatorType_OperatorTypeId",
                        column: x => x.OperatorTypeId,
                        principalTable: "ArticleOperationOperatorType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticleOperation_ArticleOperationTargetType_SourceTypeId",
                        column: x => x.SourceTypeId,
                        principalTable: "ArticleOperationTargetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleOperation_ArticleOperationType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ArticleOperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
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
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FinishTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_ArticleState_StateId",
                        column: x => x.StateId,
                        principalTable: "ArticleState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_ArticleType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ArticleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProperty_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSubscriber",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FanId = table.Column<string>(nullable: false),
                    FollowId = table.Column<string>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscriber_Users_FanId",
                        column: x => x.FanId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSubscriber_Users_FollowId",
                        column: x => x.FollowId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleComment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentCommentId = table.Column<long>(nullable: true),
                    ArticleId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    StateId = table.Column<int>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false),
                    CreatorId = table.Column<string>(maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleComment_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleComment_ArticleCommentState_StateId",
                        column: x => x.StateId,
                        principalTable: "ArticleCommentState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleProperty",
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
                    table.PrimaryKey("PK_ArticleProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleProperty_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<Guid>(nullable: false),
                    IsForever = table.Column<bool>(nullable: false),
                    ExpirationTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleTop_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false),
                    IsCancel = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavorite_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavorite_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_StateId",
                table: "Article",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_TypeId",
                table: "Article",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_ArticleId",
                table: "ArticleComment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComment_StateId",
                table: "ArticleComment",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOperation_OperatorTypeId",
                table: "ArticleOperation",
                column: "OperatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOperation_SourceTypeId",
                table: "ArticleOperation",
                column: "SourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleOperation_TypeId",
                table: "ArticleOperation",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleProperty_ArticleId",
                table: "ArticleProperty",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTop_ArticleId",
                table: "ArticleTop",
                column: "ArticleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorite_ArticleId",
                table: "UserFavorite",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorite_UserId",
                table: "UserFavorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProperty_UserId",
                table: "UserProperty",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriber_FanId",
                table: "UserSubscriber",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriber_FollowId",
                table: "UserSubscriber",
                column: "FollowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleComment");

            migrationBuilder.DropTable(
                name: "ArticleOperation");

            migrationBuilder.DropTable(
                name: "ArticleProperty");

            migrationBuilder.DropTable(
                name: "ArticleTop");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserFavorite");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserProperty");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserSubscriber");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "ArticleCommentState");

            migrationBuilder.DropTable(
                name: "ArticleOperationOperatorType");

            migrationBuilder.DropTable(
                name: "ArticleOperationTargetType");

            migrationBuilder.DropTable(
                name: "ArticleOperationType");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ArticleState");

            migrationBuilder.DropTable(
                name: "ArticleType");
        }
    }
}
