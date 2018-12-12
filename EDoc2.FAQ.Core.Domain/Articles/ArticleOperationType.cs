using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public enum ArticleOperationType
    {
        [DisplayName("赞")]
        Like = 1 << 0,

        [DisplayName("踩")]
        Dislike = 1 << 1,

        [DisplayName("举报")]
        Report = 1 << 2,

        [DisplayName("查看")]
        View = 1 << 3,

        [DisplayName("删除")]
        Delete = 1 << 4
    }

    //public class ArticleOperationType : Enumeration
    //{
    //    public static ArticleOperationType Like = new ArticleOperationType(1, "赞");
    //    public static ArticleOperationType Dislike = new ArticleOperationType(2, "踩");
    //    public static ArticleOperationType Report = new ArticleOperationType(3, "举报");
    //    public static ArticleOperationType View = new ArticleOperationType(4, "查看");
    //    public static ArticleOperationType Delete = new ArticleOperationType(5, "删除");

    //    public ArticleOperationType() { }

    //    public ArticleOperationType(int id, string name) : base(id, name) { }

    //    public static IEnumerable<ArticleOperationType> List() => new[] { Like, Dislike, Report, View, Delete };

    //    public static ArticleOperationType FromName(string name)
    //    {
    //        var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }

    //    public static ArticleOperationType From(int id)
    //    {
    //        var state = List().SingleOrDefault(s => s.Id == id);
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }
    //}
}
