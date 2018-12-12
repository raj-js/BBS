using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public enum ArticleOperationTargetType
    {
        [DisplayName("帖子")]
        Article = 1 << 0,

        [DisplayName("评论")]
        Comment = 1 << 1
    }

    //public class ArticleOperationTargetType: Enumeration
    //{
    //    public static ArticleOperationTargetType Article = new ArticleOperationTargetType(1, "帖子");
    //    public static ArticleOperationTargetType Comment = new ArticleOperationTargetType(2, "评论");

    //    public ArticleOperationTargetType() { }

    //    public ArticleOperationTargetType(int id, string name) : base(id, name) { }

    //    public static IEnumerable<ArticleOperationTargetType> List() => new[] { Article, Comment };

    //    public static ArticleOperationTargetType FromName(string name)
    //    {
    //        var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }

    //    public static ArticleOperationTargetType From(int id)
    //    {
    //        var state = List().SingleOrDefault(s => s.Id == id);
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }
    //}
}
