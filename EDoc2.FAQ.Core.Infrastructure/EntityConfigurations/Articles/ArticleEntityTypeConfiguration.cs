using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Articles
{
    class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> b)
        {
            b.Ignore(e => e.DomainEvents);

            b.HasKey(e => e.Id);

            b.Property(e => e.Title).HasMaxLength(128).IsRequired();

            b.Property(e => e.Summary).HasMaxLength(256).IsRequired(false);

            b.Property(e => e.Content).IsRequired();

            b.Property(e => e.Keywords).HasMaxLength(50).IsRequired();

            b.Property(e => e.CanComment).HasDefaultValue(true);

            b.Property(e => e.CreationTime).IsRequired();

            b.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
