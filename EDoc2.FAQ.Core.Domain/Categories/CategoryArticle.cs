using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Categories
{
    /// <summary>
    /// 类别文章
    /// </summary>
    public class CategoryArticle : Entity
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        public virtual Article Article { get; set; }
    }
}
