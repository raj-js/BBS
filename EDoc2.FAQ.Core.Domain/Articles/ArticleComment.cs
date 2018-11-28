using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using EDoc2.FAQ.Core.Domain.Articles.Events;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 文章评论
    /// </summary>
    public class ArticleComment : Entity<long>
    {
        /// <summary>
        /// 若不为 NULL , 表示此评论是回复其他评论
        /// 若为 NULL, 表示此评论是回复文章
        /// </summary>
        public long? ParentCommentId { get; set; }

        /// <summary>
        /// 文章编号
        /// </summary>
        public Guid ArticleId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public virtual ArticleCommentState State { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 所属文章
        /// </summary>
        public virtual Article Article { get; set; }

        #region 状态事件

        /// <summary>
        /// 评论审批驳回
        /// </summary>
        /// <param name="operatorId"></param>
        public void SetRejected(string operatorId)
        {
            if (State.Id != ArticleCommentState.Auditing.Id) return;

            AddDomainEvent(new ArticleCommentStateChangedToRejectedDomainEvent(this, operatorId));
        }

        /// <summary>
        /// 评论审核通过
        /// </summary>
        /// <param name="operatorId"></param>
        public void SetValidated(string operatorId)
        {
            if (State.Id != ArticleCommentState.Auditing.Id) return;

            AddDomainEvent(new ArticleCommentStateChangedToValidatedDomainEvent(this, operatorId));
        }

        #endregion
    }
}
