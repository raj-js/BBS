using System;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Models.ArticleAggregate
{
    public class ArticleState : Enumeration
    {
        public static ArticleState Draft = new ArticleState(1, "草稿");
        public static ArticleState Approving = new ArticleState(2, "审批中");
        public static ArticleState Published = new ArticleState(3, "已发布");
        public static ArticleState Deleted = new ArticleState(4, "已删除");

        public ArticleState() { }

        public ArticleState(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ArticleState> List() => new[] { Draft, Approving, Published, Deleted };

        public static ArticleState FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static ArticleState From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
