using System;
using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 文章状态
    /// </summary>
    [Flags]
    public enum ArticleState
    {
        [DisplayName("草稿")]
        Draft = 1 << 0,

        [DisplayName("审核中")]
        Auditing = 1 << 1,

        [DisplayName("驳回")]
        Rejected = 1 << 2,

        [DisplayName("已发布")]
        Published = 1 << 3,

        [DisplayName("未结帖")]
        UnSolved = 1 << 4,

        [DisplayName("已结贴")]
        Solved = 1 << 5,

        [DisplayName("无满意结贴")]
        Unsatisfactory = 1 << 6,

        [DisplayName("已删除")]
        Deleted = 1 << 7
    }

    //public class ArticleState : Enumeration
    //{
    //    public static ArticleState Draft = new ArticleState(1, "草稿");
    //    public static ArticleState Auditing = new ArticleState(2, "审核中");
    //    public static ArticleState Rejected = new ArticleState(3, "驳回");

    //    /// <summary>
    //    /// 针对审核通过的文章
    //    /// </summary>
    //    public static ArticleState Published = new ArticleState(4, "已发布");

    //    /// <summary>
    //    /// 针对审核通过的问题
    //    /// </summary>
    //    public static ArticleState UnSolved = new ArticleState(5, "未结帖");

    //    public static ArticleState Solved = new ArticleState(6, "已结贴");
    //    public static ArticleState Unsatisfactory = new ArticleState(7, "无满意结贴");
    //    public static ArticleState Deleted = new ArticleState(8, "已删除");

    //    public ArticleState() { }

    //    public ArticleState(int id, string name)
    //        : base(id, name)
    //    {
    //    }

    //    public static IEnumerable<ArticleState> List() => new[] { Draft, Auditing, Rejected, Published, UnSolved, Solved, Unsatisfactory, Deleted };

    //    public static ArticleState FromName(string name)
    //    {
    //        var state = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }

    //    public static ArticleState From(int id)
    //    {
    //        var state = List().SingleOrDefault(s => s.Id == id);
    //        if (state == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return state;
    //    }
    //}
    }
