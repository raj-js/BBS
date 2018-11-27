using System;
using MediatR;

namespace EDoc2.FAQ.Core.Domain.Articles.Events
{
    public class ArticleCommentStateChangedToRejectedDomainEvent : INotification
    {
        public ArticleComment Comment { get; set; }

        public string OperatorId { get; set; }

        public ArticleCommentStateChangedToRejectedDomainEvent(ArticleComment comment, string operatorId)
        {
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
            OperatorId = operatorId ?? throw new ArgumentNullException(nameof(operatorId));
        }
    }
}
