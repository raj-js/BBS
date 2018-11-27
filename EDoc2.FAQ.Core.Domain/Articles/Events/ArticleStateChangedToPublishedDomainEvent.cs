using System;
using MediatR;

namespace EDoc2.FAQ.Core.Domain.Articles.Events
{
    /// <summary>
    /// 文字状态改为发布
    /// 需要修改积分
    /// </summary>
    public class ArticleStateChangedToPublishedDomainEvent : INotification
    {
        public Article Article { get; set; }

        public string OperatorId { get; set; }

        public DateTime AuditingTime { get; set; }

        /// <summary>
        /// 发布文章
        /// </summary>
        /// <param name="article">文章实体</param>
        /// <param name="operatorId">
        /// </param>
        public ArticleStateChangedToPublishedDomainEvent(Article article, string operatorId)
        {
            Article = article;
            OperatorId = operatorId;
            AuditingTime = DateTime.Now;
        }
    }
}
