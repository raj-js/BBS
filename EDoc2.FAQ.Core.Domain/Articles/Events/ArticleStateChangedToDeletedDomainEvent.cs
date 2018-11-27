using MediatR;

namespace EDoc2.FAQ.Core.Domain.Articles.Events
{
    public class ArticleStateChangedToDeletedDomainEvent : INotification
    {
        public Article Article { get; set; }

        public string OperatorId { get; set; }

        public ArticleStateChangedToDeletedDomainEvent(Article article, string operatorId)
        {
            Article = article;
            OperatorId = operatorId;
        }
    }
}
