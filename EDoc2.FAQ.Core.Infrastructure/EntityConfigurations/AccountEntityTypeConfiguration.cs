using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations
{
    class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.IsMuted);

            b.Property(e => e.Nickname).HasMaxLength(50);

            b.Property(e => e.Gender);

            b.Property(e => e.City).HasMaxLength(128);

            b.Property(e => e.Signature).HasMaxLength(256);

            b.Property(e => e.JoinDate);

            b.HasMany(e => e.UserClaims)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            b.HasMany(e => e.UserLogins)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            b.HasMany(e => e.UserTokens)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            b.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            b.HasMany(e => e.UserProperties)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            b.HasMany(e => e.UserFavorites)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            b.HasMany(e => e.UserFollows)
                .WithOne(e => e.Follow)
                .HasForeignKey(e => e.FollowId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(e => e.UserFans)
                .WithOne(e => e.Fan)
                .HasForeignKey(e => e.FanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    class UserLoginEntityTypeConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> b)
        {
            b.HasKey(e => e.UserId);
        }
    }

    class UserRoleEntityTypeConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> b)
        {
            b.HasKey(e => new { e.UserId, e.RoleId });
        }
    }

    class UserTokenEntityTypeConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> b)
        {
            b.HasKey(e => e.UserId);
        }
    }

    class UserSubscriberEntityTypeConfiguration : IEntityTypeConfiguration<UserSubscriber>
    {
        public void Configure(EntityTypeBuilder<UserSubscriber> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.FanId).IsRequired();

            b.Property(e => e.FollowId).IsRequired();

            b.Property(e => e.OperationTime).IsRequired();

            b.Property(e => e.IsCancel).IsRequired();
        }
    }
}
