//using EDoc2.FAQ.Core.Domain.Articles;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;

//namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Articles
//{
//    class ArticleOperationOperatorTypeEntityTypeConfiguration : IEntityTypeConfiguration<ArticleOperationOperatorType>
//    {
//        public void Configure(EntityTypeBuilder<ArticleOperationOperatorType> b)
//        {
//            b.HasKey(e => e.Id);

//            b.Property(e => e.Id)
//                .HasDefaultValue(1)
//                .ValueGeneratedNever()
//                .IsRequired();

//            b.Property(e => e.Name)
//                .HasMaxLength(50)
//                .IsRequired();
//        }
//    }
//}
