using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Data.Discuss
{
    public class ArticleComment
    {
        public string Id { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// 是否为回复评论
        /// </summary>
        public bool IsReplyToComment { get; set; }

        /// <summary>
        /// 被回复的评论编号
        /// </summary>
        public string ReplyCommentId { get; set; }


        public string ArticleId { get; set; }

        /// <summary>
        /// 赞
        /// </summary>
        public int Goods { get; set; }

        /// <summary>
        /// 踩
        /// </summary>
        public int Bads { get; set; }

        /// <summary>
        /// 回复人编号
        /// </summary>
        public string FromUserId { get; set; }

        public DateTime ReplyDate { get; set; }

        public virtual Article Article { get; set; }

        public virtual AppUser FromUser { get; set; }

        public virtual ArticleComment ReplyToComment { get; set; }

        public virtual ICollection<CommentOp> CommentOps { get; set; }

        /// <summary>
        /// 是否被采纳（勿改）
        /// </summary>
        public bool IsAdopt()
        {
            return Id.Equals(Article?.AdoptCommentId);
        }
    }
}
