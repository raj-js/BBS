using System;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public class ArticleState : Enumeration
    {
        public static ArticleState Draft = new ArticleState(1, "草稿");
        public static ArticleState Auditing = new ArticleState(2, "审核中");
        public static ArticleState Rejected = new ArticleState(3, "驳回");
        public static ArticleState Published = new ArticleState(4, "已发布");
        public static ArticleState UnSolved = new ArticleState(5, "未结帖");
        public static ArticleState Solved = new ArticleState(6, "已结贴");
        public static ArticleState Unsatisfactory = new ArticleState(7, "无满意结贴");
        public static ArticleState Deleted = new ArticleState(8, "已删除");

        public ArticleState() { }

        public ArticleState(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ArticleState> List() => new[] { Draft, Auditing, Rejected, Published, UnSolved, Solved, Unsatisfactory, Deleted };

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
