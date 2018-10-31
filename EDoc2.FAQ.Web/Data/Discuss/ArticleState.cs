using System;
using System.ComponentModel.DataAnnotations;

namespace EDoc2.FAQ.Web.Data.Discuss
{
    [Flags]
    public enum ArticleState
    {
        [Display(Name = "草稿")]
        Draft = 1 << 0,
        [Display(Name = "发布")]
        Published = 1 << 1,
        [Display(Name = "删除")]
        Deleted = 1 << 2,
        [Display(Name = "锁定")]
        Locked = 1 << 3,
        [Display(Name = "审核")]
        Auditing = 1 << 4,
        [Display(Name = "结贴")]
        Solved = 1 << 5,
        [Display(Name = "未结")]
        NotSolve = 1 << 6,
        [Display(Name = "精华")]
        Wonderful = 1 << 7,
        [Display(Name = "无满意结贴")]
        Dissatisfied = 1 << 8
    }
}
