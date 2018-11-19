using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 文章属性 ps:可存储更新较为频繁的字段
    /// </summary>
    public class ArticleProperty : Entity
    {

        #region Properties

        /// <summary>
        /// 访问量
        /// </summary>
        public const string PvNumber = "PV";

        /// <summary>
        /// 赞数
        /// </summary>
        public const string PraiseNumber = "Praise";

        /// <summary>
        /// 踩数
        /// </summary>
        public const string TreadNumber = "Tread";

        /// <summary>
        /// 悬赏分
        /// </summary>
        public const string RewardScore = "Reward";

        /// <summary>
        /// 采纳回复编号
        /// </summary>
        public const string AdoptCommentId = "Adopt";

        #endregion


        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 所属文章
        /// </summary>
        public virtual Article Article { get; set; }
    }
}
