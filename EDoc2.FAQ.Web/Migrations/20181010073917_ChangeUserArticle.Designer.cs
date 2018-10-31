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
    [Migration("20181010073917_ChangeUserArticle")]
    partial class ChangeUserArticle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

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

                    b.Property<string>("SpeColId")
                        .IsRequired();

                    b.Property<int>("State");

                    b.Property<string>("Title");

                    b.Property<DateTime?>("TopDate");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.HasIndex("SpeColId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.ArticleComment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleId")
                        .IsRequired();

                    b.Property<string>("Content");

                    b.Property<string>("FromUserId");

                    b.Property<bool>("IsReplyToUser");

                    b.Property<DateTime>("ReplyDate");

                    b.Property<string>("ToUserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("ArticleComments");
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

            modelBuilder.Entity("EDoc2.FAQ.Web.Data.Discuss.Article", b =>
                {
                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "Publisher")
                        .WithMany("Articles")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Web.Data.Discuss.ArticleSpeCol", "SpeCol")
                        .WithMany("Articles")
                        .HasForeignKey("SpeColId")
                        .OnDelete(DeleteBehavior.Cascade);
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

                    b.HasOne("EDoc2.FAQ.Web.Data.Identity.AppUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
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
