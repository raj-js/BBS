using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories.EntityConfigurations
{
    class ArticlePropertyEntityTypeConfiguration : IEntityTypeConfiguration<ArticleProperty>
    {
        public void Configure(EntityTypeBuilder<ArticleProperty> b)
        {
            b.Ignore(e => e.DomainEvents);

            b.Property(e => e.Name).HasMaxLength(50).IsRequired();

            b.Property(e => e.Value).IsRequired(false);

            b.HasOne(e => e.Article)
                .WithMany(e => e.Properties)
                .IsRequired()
                .HasForeignKey(e => e.ArticleId);
        }
    }
}
