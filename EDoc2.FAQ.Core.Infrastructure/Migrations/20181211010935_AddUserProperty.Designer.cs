﻿// <auto-generated />
using System;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EDoc2.FAQ.Core.Infrastructure.Migrations
{
    [DbContext(typeof(CommunityContext))]
    [Migration("20181211010935_AddUserProperty")]
    partial class AddUserProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("City")
                        .HasMaxLength(128);

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsMuted");

                    b.Property<DateTime>("JoinDate");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nickname")
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Signature")
                        .HasMaxLength(256);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserClaim", b =>
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

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserFavorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ArticleId");

                    b.Property<bool>("IsCancel");

                    b.Property<DateTime>("OperationTime");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFavorite");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserLogin", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.HasKey("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserProperty");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserSubscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FanId")
                        .IsRequired();

                    b.Property<string>("FollowId")
                        .IsRequired();

                    b.Property<bool>("IsCancel");

                    b.Property<DateTime>("OperationTime");

                    b.HasKey("Id");

                    b.HasIndex("FanId");

                    b.HasIndex("FollowId");

                    b.ToTable("UserSubscriber");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Applications.Application", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("IconBase64");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Application");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Applications.ApplicationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ApplicationId");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ApplicationSetting");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanComment")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("FinishTime");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("StateId");

                    b.Property<string>("Summary")
                        .HasMaxLength(256);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.HasIndex("TypeId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ArticleId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Dislikes");

                    b.Property<int>("Likes");

                    b.Property<long?>("ParentCommentId");

                    b.Property<int?>("StateId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("StateId");

                    b.ToTable("ArticleComment");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleCommentState", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ArticleCommentState");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsCancel");

                    b.Property<DateTime>("OperationTime");

                    b.Property<string>("Operator")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("OperatorTypeId");

                    b.Property<int>("SourceTypeId");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("TypeId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OperatorTypeId");

                    b.HasIndex("SourceTypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("ArticleOperation");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperationOperatorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ArticleOperationOperatorType");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperationTargetType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ArticleOperationTargetType");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperationType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ArticleOperationType");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ArticleId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("ArticleProperty");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleState", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ArticleState");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleTop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("ArticleId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<DateTime?>("ExpirationTime");

                    b.Property<bool>("IsCancel");

                    b.Property<bool>("IsForever");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId")
                        .IsUnique();

                    b.ToTable("ArticleTop");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ArticleType");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.RoleClaim", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserClaim", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserFavorite", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "User")
                        .WithMany("UserFavorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserLogin", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserProperty", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "User")
                        .WithMany("UserProperties")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserRole", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserSubscriber", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "Fan")
                        .WithMany("UserFollows")
                        .HasForeignKey("FanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "Follow")
                        .WithMany("UserFans")
                        .HasForeignKey("FollowId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Accounts.UserToken", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Accounts.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.Applications.ApplicationSetting", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.Applications.Application", "Application")
                        .WithMany("Settings")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.Article", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleComment", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleCommentState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperation", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperationOperatorType", "OperatorType")
                        .WithMany()
                        .HasForeignKey("OperatorTypeId");

                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperationTargetType", "TargetType")
                        .WithMany()
                        .HasForeignKey("SourceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleOperationType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleProperty", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.Article", "Article")
                        .WithMany("Properties")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleTop", b =>
                {
                    b.HasOne("EDoc2.FAQ.Core.Domain.SubCategoryArticles.Article", "Article")
                        .WithOne("ArticleTop")
                        .HasForeignKey("EDoc2.FAQ.Core.Domain.SubCategoryArticles.ArticleTop", "ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
