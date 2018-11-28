using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Modules
{
    /// <summary>
    /// 文章板块
    /// </summary>
    public class Module : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 板块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 板块描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 帖子数量
        /// </summary>
        public int ArticleCount { get; private set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// 版主编号
        /// </summary>
        public string ModeratorId { get; set; }

        /// <summary>
        /// 版主
        /// </summary>
        public virtual User Moderator { get; set; }

        /// <summary>
        /// 启用/禁用当前模块
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// 创建新的板块
        /// </summary>
        /// <param name="name">模块名称</param>
        /// <param name="description">模块描述</param>
        /// <returns></returns>
        public Module(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ArticleCount = 0;
            CreateDate = DateTime.Now;
        }
    }
}
