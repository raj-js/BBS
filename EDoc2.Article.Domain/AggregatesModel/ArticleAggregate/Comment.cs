using System;
using EDoc2.Article.Domain.SeedWork;

namespace EDoc2.Article.Domain.AggregatesModel.ArticleAggregate
{
    public class Comment : Entity
    {
        public int CommentId { get; set; }
        public string ReplierId { get; set; }
        public string Content { get; set; }
        public string ReplyToId { get; set; }
        public DateTime ReplyTime { get; set; }

        public Comment()
        {

        }

        public Comment(int commentId, string replierId, string content, string replyToId, DateTime replyTime)
        {
            CommentId = commentId;
            ReplierId = replierId;
            Content = content;
            ReplyTime = replyTime;
            ReplyToId = replyToId;
        }
    }
}
