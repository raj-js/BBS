﻿// <auto-generated />
using System;
using EDoc2.FAQ.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EDoc2.FAQ.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20181024070650_AddRelationCategories")]
    partial class AddRelationCategories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.DailySignIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientIp");

                    b.Property<DateTime>("SignInTime");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DailySignIns");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.LogScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<int>("Score");

                    b.Property<int>("Total");

                    b.Property<string>("Type");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LogScores");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.Notice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("SenderId");

                    b.Property<int>("State");

                    b.Property<int>("What");

                    b.Property<DateTime>("When");

                    b.Property<int>("Where");

                    b.Property<int>("Who");

                    b.Property<string>("WhoId");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.NoticeReceive", b =>
                {
                    b.Property<string>("NoticeId");

                    b.Property<string>("ReveiverId");

                    b.Property<DateTime?>("ReadDate");

                    b.Property<int>("State");

                    b.HasKey("NoticeId", "ReveiverId");

                    b.HasIndex("ReveiverId");

                    b.ToTable("NoticeReceive");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<DateTime?>("ProcessDate");

                    b.Property<string>("ProcessMsg")
                        .HasMaxLength(1024);

                    b.Property<DateTime>("ReportDate");

                    b.Property<int>("ReportTargetType");

                    b.Property<string>("ReporterId")
                        .IsRequired();

                    b.Property<int>("Result");

                    b.Property<int>("SubType");

                    b.Property<string>("TargetId");

                    b.HasKey("Id");

                    b.HasIndex("ReporterId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdoptCommentId");

                    b.Property<string>("ArticleSpeColId");

                    b.Property<string>("Content");

                    b.Property<DateTime?>("CreamDate");

                    b.Property<bool>("IsCream");

                    b.Property<bool>("IsCreamTimeout");

                    b.Property<bool>("IsTop");

                    b.Property<bool>("IsTopTimeout");

                    b.Property<string>("Labels");

                    b.Property<DateTime?>("PublishDate");

                    b.Property<string>("PublisherId")
                        .IsRequired();

                    b.Property<int>("Replies");

                    b.Property<int>("RewardScore");

                    b.Property<int>("State");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("TopDate");

                    b.Property<bool>("UseStorage");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("ArticleSpeColId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleId");

                    b.Property<string>("CategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ArticleCategories");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleComment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleId")
                        .IsRequired();

                    b.Property<int>("Bads");

                    b.Property<string>("Content");

                    b.Property<string>("FromUserId");

                    b.Property<int>("Goods");

                    b.Property<bool>("IsReplyToComment");

                    b.Property<string>("ReplyCommentId");

                    b.Property<DateTime>("ReplyDate");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ReplyCommentId");

                    b.ToTable("ArticleComments");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleFavorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleId")
                        .IsRequired();

                    b.Property<DateTime>("OperateDate");

                    b.Property<int>("State");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleFavorites");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleLabel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleId")
                        .IsRequired();

                    b.Property<string>("LabelId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("LabelId");

                    b.ToTable("ArticleLabels");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleSpeCol", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Display");

                    b.HasKey("Id");

                    b.ToTable("ArticleSpeCols");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Display");

                    b.Property<int>("SubCategory");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.CommentOp", b =>
                {
                    b.Property<string>("CommentId");

                    b.Property<string>("OperatorId");

                    b.Property<DateTime>("OperateDate");

                    b.Property<int>("Type");

                    b.HasKey("CommentId", "OperatorId");

                    b.HasIndex("OperatorId");

                    b.ToTable("CommentOps");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.Label", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Display");

                    b.HasKey("Id");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("CustomTag")
                        .HasMaxLength(256);

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.DailySignIn", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "User")
                        .WithMany("DailySignIns")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.LogScore", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "User")
                        .WithMany("LogScores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.Notice", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.NoticeReceive", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Common.Notice", "Notice")
                        .WithMany("Receivers")
                        .HasForeignKey("NoticeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "Receiver")
                        .WithMany("NoticeReceives")
                        .HasForeignKey("ReveiverId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Common.Report", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.Article", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.ArticleSpeCol")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleSpeColId");

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "Publisher")
                        .WithMany("Articles")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleCategory", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.Article", "Article")
                        .WithMany("ArticleCategories")
                        .HasForeignKey("ArticleId");

                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleComment", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "FromUser")
                        .WithMany("ArticleComments")
                        .HasForeignKey("FromUserId");

                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.ArticleComment", "ReplyToComment")
                        .WithMany()
                        .HasForeignKey("ReplyCommentId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleFavorite", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.Article", "Article")
                        .WithMany("ArticleFavorites")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "AppUser")
                        .WithMany("FavoriteArticles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleLabel", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.Article", "Article")
                        .WithMany("ArticleLabels")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.Label", "Label")
                        .WithMany("ArticleLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.CommentOp", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.ArticleComment", "Comment")
                        .WithMany("CommentOps")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "Operator")
                        .WithMany("CommentOps")
                        .HasForeignKey("OperatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppRoleClaim", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserClaim", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserLogin", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserRole", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Identity.AppUserToken", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}