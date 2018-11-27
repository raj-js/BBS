using System;
using MediatR;

namespace EDoc2.FAQ.Core.Domain.Articles.Events
{
    public class ArticleStateChangedToRejectedDomainEvent : INotification
    {
        public Article Article { get; set; }

        public string AuditorId { get; set; }

        public DateTime AuditingTime { get; set; }

        public ArticleStateChangedToRejectedDomainEvent(Article article, string auditorId)
        {
            Article = article;
            AuditorId = auditorId;
            AuditingTime = DateTime.Now;
        }
    }
}
