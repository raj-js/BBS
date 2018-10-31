using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EDoc2.FAQ.Web.Data
{
    public class AppDbContext : IdentityDbContext<
        AppUser, AppRole, string, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>

    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleComment> ArticleComments { get; set; }

        public DbSet<Notice> Notices { get; set; }

        public DbSet<CommentOp> CommentOps { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<DailySignIn> DailySignIns { get; set; }

        public DbSet<LogScore> LogScores { get; set; }

        public DbSet<ArticleFavorite> ArticleFavorites { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(b =>
            {
                b.Property(e => e.CustomTag).HasMaxLength(256).IsRequired(false);

                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.Articles)
                    .WithOne(e => e.Publisher)
                    .HasForeignKey(ur => ur.PublisherId)
                    .IsRequired();

                b.HasMany(e => e.ArticleComments)
                    .WithOne(e => e.FromUser)
                    .HasForeignKey(ur => ur.FromUserId);

                b.HasMany(e => e.FavoriteArticles)
                    .WithOne(e => e.AppUser)
                    .HasForeignKey(af => af.UserId);

                b.HasMany(e => e.NoticeReceives)
                    .WithOne(e => e.Receiver)
                    .HasForeignKey(af => af.ReveiverId);

                b.HasMany(e => e.CommentOps)
                    .WithOne(e => e.Operator)
                    .HasForeignKey(e => e.OperatorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<AppRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(uc => uc.RoleId)
                    .IsRequired();

                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(e => e.RoleId)
                    .IsRequired();
            });

            builder.Entity<Article>(b =>
            {
                b.HasKey(e => e.Id);

                b.HasMany(e => e.Comments)
                    .WithOne(c => c.Article)
                    .HasForeignKey(ec => ec.ArticleId)
                    .IsRequired();

                b.HasMany(e => e.ArticleFavorites)
                    .WithOne(a => a.Article)
                    .HasForeignKey(af => af.ArticleId)
                    .IsRequired();
            });

            builder.Entity<ArticleComment>(b =>
            {
                b.HasKey(e => e.Id);

                b.HasOne(ac => ac.ReplyToComment)
                    .WithMany()
                    .HasForeignKey(e => e.ReplyCommentId);
            });

            builder.Entity<CommentOp>(b =>
            {
                b.HasKey(e => new { e.CommentId, e.OperatorId });

                b.HasOne(e => e.Comment)
                    .WithMany(e => e.CommentOps)
                    .HasForeignKey(e => e.CommentId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Report>(b =>
            {
                b.HasKey(e => e.Id);

                b.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .IsRequired(false);

                b.Property(e => e.ProcessMsg)
                    .HasMaxLength(1024)
                    .IsRequired(false);

                b.HasOne(e => e.Reporter)
                    .WithMany()
                    .HasForeignKey(e => e.ReporterId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<NoticeReceive>(b =>
            {
                b.HasKey(e => new { e.NoticeId, e.ReveiverId });
            });

            builder.Entity<LogScore>(b =>
            {
                b.HasKey(e => e.Id);

                b.HasOne(e => e.User)
                    .WithMany(e => e.LogScores)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });

            builder.Entity<DailySignIn>(b =>
            {
                b.HasKey(e => e.Id);

                b.HasOne(e => e.User)
                    .WithMany(e => e.DailySignIns)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });
        }
    }
}
