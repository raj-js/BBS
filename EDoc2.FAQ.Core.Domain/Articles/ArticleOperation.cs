using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public class ArticleOperation : Entity
    {
        public string OperatorId { get; set; }

        public string SourceId { get; set; }

        public ArticleOperationSourceType SourceType  { get; set; }

        public ArticleOperationType Type { get; set; }

        public DateTime OperationTime { get; set; }
    }
}
