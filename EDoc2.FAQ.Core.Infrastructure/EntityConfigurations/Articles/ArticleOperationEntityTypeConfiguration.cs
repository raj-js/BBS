using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations
{
    public class ArticleOperationEntityTypeConfiguration : IEntityTypeConfiguration<ArticleOperation>
    {
        public void Configure(EntityTypeBuilder<ArticleOperation> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.OperatorId)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(e => e.SourceId)
                .HasMaxLength(50)
                .IsRequired();

            b.HasOne(e => e.SourceType)
                .WithMany()
                .IsRequired()
                .HasForeignKey("SourceTypeId");

            b.HasOne(e => e.Type)
                .WithMany()
                .IsRequired()
                .HasForeignKey("TypeId");

            b.Property(e => e.OperationTime)
                .IsRequired();

            b.Property(e => e.IsCancel)
                .IsRequired();
        }
    }
}
