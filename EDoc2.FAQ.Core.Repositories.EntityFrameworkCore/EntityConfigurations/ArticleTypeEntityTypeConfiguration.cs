using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations
{
    class ArticleTypeEntityTypeConfiguration : IEntityTypeConfiguration<ArticleType>
    {
        public void Configure(EntityTypeBuilder<ArticleType> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            b.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
