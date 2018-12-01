using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public class ArticleType : Enumeration
    {
        public static ArticleType Question = new ArticleType(1, "提问");
        public static ArticleType Article = new ArticleType(2, "文章");

        public ArticleType() { }

        public ArticleType(int id, string name) : base(id, name) { }

        public static IEnumerable<ArticleType> List() => new[] { Question, Article };

        public static ArticleType FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static ArticleType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
