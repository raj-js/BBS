using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Articles
{
    class ArticleTopEntityTypeConfiguration : IEntityTypeConfiguration<ArticleTop>
    {
        public void Configure(EntityTypeBuilder<ArticleTop> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.IsForever)
                .IsRequired();

            b.Property(e => e.ExpirationTime)
                .IsRequired(false);

            b.Property(e => e.CreationTime)
                .IsRequired();

            b.Property(e => e.IsCancel)
                .IsRequired();

            b.Property(e => e.ArticleId)
                .IsRequired();
        }
    }
}
