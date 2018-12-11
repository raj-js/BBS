using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 置顶文章
    /// </summary>
    public class ArticleTop : Entity
    {
        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 是否永久有效
        /// </summary>
        public bool IsForever { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        public virtual Article Article { get; set; }
    }
}
