using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public class ArticleOperationSourceType: Enumeration
    {
        public static ArticleOperationSourceType Article = new ArticleOperationSourceType(1, "帖子");
        public static ArticleOperationSourceType Comment = new ArticleOperationSourceType(2, "评论");

        public ArticleOperationSourceType() { }

        public ArticleOperationSourceType(int id, string name) : base(id, name) { }

        public static IEnumerable<ArticleOperationSourceType> List() => new[] { Article, Comment };

        public static ArticleOperationSourceType FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static ArticleOperationSourceType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
