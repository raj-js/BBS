using EDoc2.FAQ.Web.Data.Identity;
using System;
using System.Collections.Generic;

namespace EDoc2.FAQ.Web.Data.Discuss
{
    /// <summary>
    /// 问题
    /// </summary>
    public class Article
    {
        public string Id { get; set; }

        /// <summary>
        /// 发布者编号
        /// </summary>
        public string PublisherId { get; set; }

        public string Title { get; set; }

        public ArticleState State { get; set; }

        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 置顶时间
        /// </summary>
        public DateTime? TopDate { get; set; }

        /// <summary>
        /// 置顶是否过期
        /// </summary>
        public bool IsTopTimeout { get; set; }

        /// <summary>
        /// 是否加精
        /// </summary>
        public bool IsCream { get; set; }

        /// <summary>
        /// 加精时间
        /// </summary>
        public DateTime? CreamDate { get; set; }

        /// <summary>
        /// 加精是否过期
        /// </summary>
        public bool IsCreamTimeout { get; set; }

        /// <summary>
        /// 查看数
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 回复数
        /// </summary>
        public int Replies { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public string Labels { get; set; }

        /// <summary>
        /// 是否使用文件系统存储
        /// </summary>
        public bool UseStorage { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 悬赏财富值
        /// </summary>
        public int RewardScore { get; set; }

        /// <summary>
        /// 被采纳的回复编号
        /// </summary>
        public string AdoptCommentId { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public virtual AppUser Publisher { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public virtual ICollection<ArticleComment> Comments { get; set; }

        public virtual ICollection<ArticleFavorite> ArticleFavorites { get; set; }

        /// <summary>
        /// 文章分类
        /// </summary>
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
