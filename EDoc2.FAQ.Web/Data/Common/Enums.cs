using System;
using System.ComponentModel.DataAnnotations;

namespace EDoc2.FAQ.Web.Data.Common
{
    /// <summary>
    /// 举报关联类型
    /// </summary>
    public enum ReportTargetType
    {
        Article = 0,
        Comment = 1,
        User = 2
    }

    /// <summary>
    /// 举报类型
    /// </summary>
    public enum ReportSubType
    {
        [Display(Name = "假冒网站")]
        FakeWebSite = 0,

        [Display(Name = "色情")]
        Pornographic = 1,

        [Display(Name = "反动")]
        Reactionary = 2,

        [Display(Name = "传播病毒")]
        Virus = 3,

        [Display(Name = "其它")]
        Others = 4
    }

    /// <summary>
    /// 举报处理结果
    /// </summary>
    public enum ReportResult
    {
        Pass = 1,
        Failed = 2,
        Untreated = 3
    }

    /// <summary>
    /// 消息发送方类型
    /// </summary>
    public enum NoticeWho
    {
        User,
        System
    }

    /// <summary>
    /// 通知所属模块
    /// </summary>
    public enum NoticeWhere
    {
        Article,
        Register,
        ResetPassword
    }

    /// <summary>
    /// 通知描述
    /// </summary>
    public enum NoticeWhat
    {
        Reply,
        Invite,
        Follow,
        OpSuccess,
        OpFailed
    }

    /// <summary>
    /// 通知状态
    /// </summary>
    [Flags]
    public enum NoticeState
    {
        Normal = 1 >> 0,
        Deleted = 1 >> 1
    }

    /// <summary>
    /// 通知阅读状态
    /// </summary>
    [Flags]
    public enum NoticeReadState
    {
        Deleted = 1 >> 0,
        Readed = 1 >> 1,
        UnRead = 1 >> 2
    }

    [Flags]
    public enum FavoriteState
    {
        Favorite = 1 >> 0,
        Cancel = 1 >> 1
    }

    /// <summary>
    /// Article Sub Category
    /// </summary>
    public enum ArticleSubTypes
    {
        /// <summary>
        /// 所属产品
        /// </summary>
        [Display(Name = "所属产品")]
        Product,
        /// <summary>
        /// 文章类型
        /// </summary>
        [Display(Name = "文章类型")]
        Category,
        /// <summary>
        /// 文章标签
        /// </summary>
        [Display(Name = "文章标签")]
        Tag
    }
}
