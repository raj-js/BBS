using EDoc2.FAQ.Core.Domain.Models.CommentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations
{
    /// <summary>
    /// Comment 实体与数据库表结构的映射配置
    /// </summary>
    class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
