using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Categories
{
    /// <summary>
    /// 文章分类， 根节点不可选
    /// </summary>
    public class Category : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 父分类编号
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 帖子数量
        /// </summary>
        public int ArticleCount { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CteationTime { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 启用/禁用当前模块
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// 父分类
        /// </summary>
        public virtual Category Parent { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        public virtual ICollection<Category> Children { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public virtual ICollection<CategoryModerator> CategoryModerators { get; set; }

        /// <summary>
        /// 文章
        /// </summary>
        public virtual ICollection<CategoryArticle> CategoryArticles { get; set; }

        public void AddModerator(string moderatorId)
        {
            CategoryModerators = CategoryModerators ?? new List<CategoryModerator>();

            CategoryModerators.Add(new CategoryModerator
            {
                CategoryId = Id,
                ModeratorId = moderatorId,
                LastModifyTime = DateTime.Now
            });
        }

        public void RemoveModerator(string moderatorId)
        {
            var moderator = CategoryModerators?.SingleOrDefault(s => s.ModeratorId == moderatorId);
            if (moderator == null) return;

            CategoryModerators.Remove(moderator);
        }

        public void AddArticle(Guid articleId)
        {
            CategoryArticles = CategoryArticles ?? new List<CategoryArticle>();

            CategoryArticles.Add(new CategoryArticle
            {
                CategoryId = Id,
                ArticleId = articleId
            });
        }
    }
}
