using System.ComponentModel;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 评论状态
    /// </summary>
    public enum ArticleCommentState
    {
        [DisplayName("审核中")]
        Auditing = 1 << 0,

        [DisplayName("驳回")]
        Rejected = 1 << 1,

        [DisplayName("生效")]
        Validated = 1 << 2,

        [DisplayName("已删除")]
        Deleted = 1 << 3
    }
}
