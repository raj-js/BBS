using EDoc2.FAQ.Core.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Categories
{
    class CategoryModeratorEntityTypeConfiguration : IEntityTypeConfiguration<CategoryModerator>
    {
        public void Configure(EntityTypeBuilder<CategoryModerator> b)
        {
            b.HasKey(e => e.Id);

            b.HasOne(e => e.Category)
                .WithMany(e => e.CategoryModerators)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired();

            b.HasOne(e => e.Moderator)
                .WithMany()
                .HasForeignKey(e => e.ModeratorId)
                .IsRequired();
        }
    }
}
