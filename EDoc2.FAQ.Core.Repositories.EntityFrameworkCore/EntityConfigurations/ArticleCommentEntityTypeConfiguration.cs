using EDoc2.FAQ.Core.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations
{
    class ArticleCommentEntityTypeConfiguration : IEntityTypeConfiguration<ArticleComment>
    {
        public void Configure(EntityTypeBuilder<ArticleComment> b)
        {
            b.Ignore(e => e.DomainEvents);

            b.HasKey(e => new { e.Id, e.ArticleId });

            b.Property(e => e.Id)
                .HasValueGenerator(typeof(int));

            b.Property(e => e.ParentCommentId).IsRequired(false);

            b.HasOne(e => e.Article)
                .WithMany(e => e.Comments)
                .IsRequired()
                .HasForeignKey(e => e.ArticleId);

            b.Property(e => e.Content).IsRequired();

            b.HasOne(e => e.State)
                .WithMany()
                .HasForeignKey("StateId");

            b.Property(e => e.CreatorId).HasMaxLength(50).IsRequired();

            b.Property(e => e.CreationTime).IsRequired();
        }
    }
}
