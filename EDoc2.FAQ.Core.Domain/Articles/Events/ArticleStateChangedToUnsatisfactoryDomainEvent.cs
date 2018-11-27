using MediatR;

namespace EDoc2.FAQ.Core.Domain.Articles.Events
{
    public class ArticleStateChangedToUnsatisfactoryDomainEvent : INotification
    {
        public Article Article { get; set; }

        public ArticleStateChangedToUnsatisfactoryDomainEvent(Article article)
        {
            Article = article;
        }
    }
}
