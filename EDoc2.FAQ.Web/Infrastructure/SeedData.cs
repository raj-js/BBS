using EDoc2.FAQ.Web.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Extensions;
using EDoc2.FAQ.Web.Data.Common;

namespace EDoc2.FAQ.Web.Infrastructure
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider provider)
        {
            using (var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new Category
                    {
                        Id = CategoryConsts.Question,
                        Display = "提问",
                        SubCategory = ArticleSubTypes.Category
                    }, new Category
                    {
                        Id = CategoryConsts.Discuss,
                        Display = "讨论",
                        SubCategory = ArticleSubTypes.Category
                    }, new Category
                    {
                        Id = CategoryConsts.Suggest,
                        Display = "建议",
                        SubCategory = ArticleSubTypes.Category
                    }, new Category
                    {
                        Id = CategoryConsts.Share,
                        Display = "分享",
                        SubCategory = ArticleSubTypes.Category
                    }, 
                    
                    new Category
                    {
                        Id = ProductConsts.V4,
                        Display = "V4",
                        SubCategory = ArticleSubTypes.Product
                    },
                    new Category
                    {
                        Id = ProductConsts.V5,
                        Display = "V5",
                        SubCategory = ArticleSubTypes.Product
                    },
                    
                    new Category
                    {
                        Id = TagConsts.Instation,
                        Display = "安装部署",
                        SubCategory = ArticleSubTypes.Tag
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
