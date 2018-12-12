using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using EDoc2.FAQ.Core.Domain.Accounts;

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
        public ArticleCommentState State { get; set; }

        /// <summary>
        /// 赞
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// 踩
        /// </summary>
        public int Dislikes { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// 所属文章
        /// </summary>
        public virtual Article Article { get; set; }

        public static ArticleComment New(string content)
        {
            return new ArticleComment
            {
                Content = content,
                Likes = 0,
                Dislikes = 0,
                CreationTime = DateTime.Now
            };
        }

        #region 状态事件

        /// <summary>
        /// 评论审批驳回
        /// </summary>
        public void SetAuditing()
        {
            if (State.Equals(ArticleCommentState.Deleted)) return;

            State = ArticleCommentState.Auditing;
        }

        /// <summary>
        /// 评论审批驳回
        /// </summary>
        /// <param name="operatorId"></param>
        public void SetRejected(string operatorId)
        {
            if (!State.Equals(ArticleCommentState.Auditing)) return;

            State = ArticleCommentState.Rejected;
        }

        /// <summary>
        /// 评论生效
        /// </summary>
        public void SetValidated()
        {
            State = ArticleCommentState.Validated;
        }

        /// <summary>
        /// 评论删除
        /// </summary>
        /// <param name="operatorId"></param>
        public void SetDeleted(string operatorId)
        {
            State = ArticleCommentState.Deleted;
        }

        #endregion
    }
}
