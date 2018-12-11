using EDoc2.FAQ.Core.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Categories
{
    class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(e => e.Description)
                .HasMaxLength(256)
                .IsRequired(false);

            b.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false);
        }
    }
}
