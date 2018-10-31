using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.Article.Domain.SeedWork;

namespace EDoc2.Article.Domain.AggregatesModel.ArticleAggregate
{
    public class Article : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        private int _articleStatusId;
        public ArticleStatus ArticleStatus { get; private set; }

        public string AuthorId { get; private set; }
        public DateTime? PublishTime { get; private set; }

        private readonly List<Comment> _comments;
        public IReadOnlyCollection<Comment> Comments => _comments;

        public Article()
        {
            _comments = new List<Comment>();
        }

        public Article(string identity, string title, string description, int articleStatusId, string authorId, DateTime? publishTime)
            :this()
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentNullException(nameof(title));
            Description = !string.IsNullOrWhiteSpace(description) ? description : throw new ArgumentNullException(nameof(description));
            _articleStatusId = articleStatusId;
            AuthorId = authorId;
            PublishTime = publishTime;
        }

        public static Article NewArticle(string authorId)
        {
            return new Article
            {
                AuthorId = authorId,
                ArticleStatus = ArticleStatus.Draft
            };
        }

        public void AddComent(int commentId, string replierId, string content, string replyToId, DateTime replyTime)
        {
            var comment = _comments.SingleOrDefault(c => c.CommentId == commentId);
            if (comment != null)
            {
                comment.ReplierId = replierId;
                comment.Content = content;
                comment.ReplyToId = replyToId;
                comment.ReplyTime = replyTime;
            }
            else
            {
                comment = new Comment(commentId, replierId, content, replyToId, replyTime);
                _comments.Add(comment);
            }
        }
    }
}
