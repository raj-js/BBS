using EDoc2.FAQ.Core.Domain.Models.ArticleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// Article 实体与数据库表结构的映射配置
    /// </summary>
    class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
