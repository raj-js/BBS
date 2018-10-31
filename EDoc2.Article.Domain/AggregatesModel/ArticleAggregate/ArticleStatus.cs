using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.Article.Domain.Exceptions;
using EDoc2.Article.Domain.SeedWork;

namespace EDoc2.Article.Domain.AggregatesModel.ArticleAggregate
{
    public class ArticleStatus : Enumeration
    {
        public static ArticleStatus Draft = new ArticleStatus(1, nameof(Answering).ToLowerInvariant());
        public static ArticleStatus Answering = new ArticleStatus(2, nameof(Answering).ToLowerInvariant());
        public static ArticleStatus Finished = new ArticleStatus(3, nameof(Finished).ToLowerInvariant());
        public static ArticleStatus Deleted = new ArticleStatus(4, nameof(Deleted).ToLowerInvariant());

        public ArticleStatus(int id, string name)
            :base(id, name)
        {
            
        }

        public static IEnumerable<ArticleStatus> List() => new[] { Draft, Answering, Finished, Deleted };

        public static ArticleStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if(state == null)
                throw new ArticleDomainException($"Possible values for ArticleStatus: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static ArticleStatus From(int id)
        {
            var state = List()
                .SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new ArticleDomainException($"Possible values for ArticleStatus: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
