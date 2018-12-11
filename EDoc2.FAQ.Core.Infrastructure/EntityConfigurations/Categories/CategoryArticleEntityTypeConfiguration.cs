using EDoc2.FAQ.Core.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Categories
{
    class CategoryArticleEntityTypeConfiguration : IEntityTypeConfiguration<CategoryArticle>
    {
        public void Configure(EntityTypeBuilder<CategoryArticle> b)
        {
            b.HasKey(e => e.Id);

            b.HasOne(e => e.Category)
                .WithMany(e => e.CategoryArticles)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

            b.HasOne(e => e.Article)
                .WithMany()
                .HasForeignKey(e => e.ArticleId)
                .IsRequired();
        }
    }
}
