using System;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Categories
{
    /// <summary>
    /// 板块管理员
    /// </summary>
    public class CategoryModerator : Entity
    {
        /// <summary>
        /// 类别编号
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 版块管理员编号
        /// </summary>
        public string ModeratorId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// 模块管理者
        /// </summary>
        public virtual User Moderator { get; set; }
    }
}
