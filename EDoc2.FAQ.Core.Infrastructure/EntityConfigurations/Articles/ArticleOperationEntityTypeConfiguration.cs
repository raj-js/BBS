using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Articles
{
    public class ArticleOperationEntityTypeConfiguration : IEntityTypeConfiguration<ArticleOperation>
    {
        public void Configure(EntityTypeBuilder<ArticleOperation> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.Operator)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(e => e.Target)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(e => e.OperationTime)
                .IsRequired();

            b.Property(e => e.IsCancel)
                .IsRequired();
        }
    }
}
