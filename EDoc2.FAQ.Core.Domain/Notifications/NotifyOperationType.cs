using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public enum NotifyOperationType
    {
        [DisplayName("系统公告")]
        SystemNotice,

        [DisplayName("关注")]
        Follow,

        [DisplayName("回复文章/帖子")]
        ReplyArticle,

        [DisplayName("回复评论")]
        ReplyComment,

        [DisplayName("引用评论")]
        ReferComment,

        [DisplayName("文章/帖子审核通过")]
        ArticleApprovePassed,

        [DisplayName("文章/帖子审核驳回")]
        ArticleApproveRejected,

        [DisplayName("注册成功")]
        RegisterSuccess,

        [DisplayName("非常用地点登录")]
        SignInUnusualPlace,

        [DisplayName("举报文章/帖子")]
        ReportArticle,

        [DisplayName("举报评论")]
        ReportComment,

        [DisplayName("最佳回复")]
        TheBestAnswer,

        [DisplayName("授权版主")]
        GrantModerator,

        [DisplayName("撤销版主")]
        RecycleModerator
    }

    //public class NotifyOperationType : Enumeration
    //{
    //    public static NotifyOperationType SystemNotice = new NotifyOperationType(1, "系统公告");
    //    public static NotifyOperationType Follow = new NotifyOperationType(2, "关注");
    //    public static NotifyOperationType ReplyArticle = new NotifyOperationType(3, "回复文章/帖子");
    //    public static NotifyOperationType ReplyComment = new NotifyOperationType(4, "回复评论");
    //    public static NotifyOperationType ReferComment = new NotifyOperationType(5, "引用评论");
    //    public static NotifyOperationType ArticleApprovePassed = new NotifyOperationType(6, "文章/帖子审核通过");
    //    public static NotifyOperationType ArticleApproveRejected = new NotifyOperationType(7, "文章/帖子审核驳回");
    //    public static NotifyOperationType RegisterSuccess = new NotifyOperationType(8, "注册成功");
    //    public static NotifyOperationType SignInUnusualPlace = new NotifyOperationType(9, "非常用地点登录");
    //    public static NotifyOperationType ReportArticle = new NotifyOperationType(10, "举报文章/帖子");
    //    public static NotifyOperationType ReportComment = new NotifyOperationType(11, "举报评论");
    //    public static NotifyOperationType TheBestAnswer = new NotifyOperationType(12, "最佳回复");
    //    public static NotifyOperationType GrantModerator = new NotifyOperationType(13, "授权版主");
    //    public static NotifyOperationType RecycleModerator = new NotifyOperationType(14, "撤销版主");

    //    public NotifyOperationType()
    //    {
    //    }

    //    public NotifyOperationType(int id, string name)
    //    : base(id, name)
    //    {

    //    }

    //    public static IEnumerable<NotifyOperationType> List() => new[] {
    //        SystemNotice,
    //        Follow,
    //        ReplyArticle,
    //        ReplyComment,
    //        ReferComment,
    //        ArticleApprovePassed,
    //        ArticleApproveRejected,
    //        RegisterSuccess,
    //        SignInUnusualPlace,
    //        ReportArticle,
    //        ReportComment,
    //        TheBestAnswer,
    //        GrantModerator,
    //        RecycleModerator
    //    };

    //    public static NotifyOperationType FromName(string name)
    //    {
    //        var type = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));
    //        if (type == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return type;
    //    }

    //    public static NotifyOperationType From(int id)
    //    {
    //        var type = List().SingleOrDefault(s => s.Id == id);
    //        if (type == null)
    //            throw new ArgumentException($"Possible values for CardType: {string.Join(",", List().Select(s => s.Name))}");

    //        return type;
    //    }
    //}
}
