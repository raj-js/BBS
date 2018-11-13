using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Models.ArticleAggregate
{
    public class ArticleType : Enumeration
    {
        public static ArticleType Question = new ArticleType(1, "提问");
        public static ArticleType Communication = new ArticleType(2, "交流");
        public static ArticleType Suggestion = new ArticleType(3, "建议");
        public static ArticleType Share = new ArticleType(4, "分享");
        public static ArticleType Notice = new ArticleType(5, "公告");

        public ArticleType() { }

        public ArticleType(int id, string name) : base(id, name) { }

        public static IEnumerable<ArticleType> List() => new[] { Question, Communication, Suggestion, Share, Notice };

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
