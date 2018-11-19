using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 评论状态
    /// </summary>
    public class ArticleCommentState : Enumeration
    {
        public static ArticleCommentState Auditing = new ArticleCommentState(1, "审核中");
        public static ArticleCommentState Rejected = new ArticleCommentState(2, "驳回");
        public static ArticleCommentState Validated = new ArticleCommentState(3, "生效");
        public static ArticleCommentState Deleted = new ArticleCommentState(4, "已删除");

        public ArticleCommentState()
        {
        }

        public ArticleCommentState(int id, string name)
        : base(id, name)
        {

        }

        public static IEnumerable<ArticleCommentState> List() => new[] { Auditing, Rejected, Validated, Deleted };

        public static ArticleCommentState FromName(string name)
        {
            var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static ArticleCommentState From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
