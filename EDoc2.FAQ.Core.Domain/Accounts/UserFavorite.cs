using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Accounts
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
        public DateTime CollectingTime { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
