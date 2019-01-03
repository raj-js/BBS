using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Notifies
{
    /// <summary>
    /// 消息关联对象类型
    /// </summary>
    public enum NotifyRelationObjectType
    {
        [DisplayName("管理员")]
        Administrator = 1,

        [DisplayName("版主")]
        Moderator = 1 << 1,

        [DisplayName("会员")]
        Member = 1 << 2,

        [DisplayName("文章")]
        Article = 1 << 3,

        [DisplayName("问题")]
        Question = 1 << 4,

        [DisplayName("评论")]
        Comment = 1 << 5
    }

    //public class NotifyRelationObjectType : Enumeration
    //{
    //    public static NotifyRelationObjectType Administrator = new NotifyRelationObjectType(1, "管理员");
    //    public static NotifyRelationObjectType Moderator = new NotifyRelationObjectType(2, "版主");
    //    public static NotifyRelationObjectType Member = new NotifyRelationObjectType(3, "会员");
    //    public static NotifyRelationObjectType Article = new NotifyRelationObjectType(4, "文章/帖子");
    //    public static NotifyRelationObjectType Comment = new NotifyRelationObjectType(5, "评论");

    //    public NotifyRelationObjectType()
    //    {
    //    }

    //    public NotifyRelationObjectType(int id, string name)
    //    : base(id, name)
    //    {

    //    }

    //    public static IEnumerable<NotifyRelationObjectType> List() => new[] { Administrator, Moderator, Member, Article, Comment };

    //    public static NotifyRelationObjectType FromName(string name)
    //    {
    //        var type = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (type == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return type;
    //    }

    //    public static NotifyRelationObjectType From(int id)
    //    {
    //        var type = List().SingleOrDefault(s => s.Id == id);
    //        if (type == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return type;
    //    }
    //}
}
