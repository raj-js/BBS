using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    /// <summary>
    /// 用户收藏文章
    /// </summary>
    public class UserFavorite : Entity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 收藏文章
        /// </summary>
        public virtual Articles.Article  Article { get; set; }

        public UserFavorite(string userId, Guid articleId)
        {
            UserId = userId;
            ArticleId = articleId;
            OperationTime = DateTime.Now;
            IsCancel = false;
        }
    }
}
