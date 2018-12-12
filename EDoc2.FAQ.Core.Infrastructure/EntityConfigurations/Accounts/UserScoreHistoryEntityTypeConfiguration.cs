using EDoc2.FAQ.Core.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Accounts
{
    class UserScoreHistoryEntityTypeConfiguration : IEntityTypeConfiguration<UserScoreHistory>
    {
        public void Configure(EntityTypeBuilder<UserScoreHistory> b)
        {
            b.HasKey(e => e.Id);

            b.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
