using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.SeedWork;
using EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly CommunityContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ArticleRepository(CommunityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Article Update(Article entity)
        {
            return _context.Articles.Update(entity).Entity;
        }

        public Article Find(Guid id)
        {
            return _context.Articles.Find(id);
        }

        public async Task<Article> FindAsync(Guid key)
        {
            return await _context.Articles.FindAsync(key);
        }

        public Article Release(string operatorId, bool auditing, Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            if (auditing)
                article.SetAuditing(operatorId);
            else
                article.SetPublished(operatorId);

            if (article.IsTransient())
                return _context.Articles.Add(article).Entity;
            else
                return Update(article);
        }

        public Article ViewArticle(Article article, DateTime viewTime, string clientIp, string operatorId = null)
        {
            throw new NotImplementedException();
        }

        public void ReplyArticle(Guid articleId, ArticleComment comment, bool auditing)
        {
            throw new NotImplementedException();
        }

        public void ReplyArticleComment(long parentCommentId, ArticleComment comment, bool auditing)
        {
            if (parentCommentId < 0) throw new ArgumentOutOfRangeException(nameof(parentCommentId));
            if (comment == null) throw new ArgumentNullException(nameof(comment));


        }

        /// <summary>
        /// 更新文章/评论 操作
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="sourceId"></param>
        /// <param name="sourceType"></param>
        /// <param name="operationType"></param>
        private void UpdateArticleOperation(string operatorId, string sourceId, ArticleOperationSourceType sourceType, ArticleOperationType operationType)
        {
            var op = _context.ArticleOperations
               .SingleOrDefault(o => o.OperatorId == operatorId &&
               o.SourceId == sourceId &&
               o.SourceType.Id == sourceType.Id &&
               o.Type.Id == operationType.Id);

            if (op == null)
            {
                op = new ArticleOperation
                {
                    OperatorId = operatorId,
                    SourceId = sourceId,
                    IsCancel = false,
                    SourceType = sourceType,
                    Type = operationType,
                    OperationTime = DateTime.Now
                };
                _context.ArticleOperations.Add(op);
            }
            else
            {
                op.IsCancel = false;
                op.OperationTime = DateTime.Now;
                _context.ArticleOperations.Update(op);
            }
        }

        public void LikeArticle(string operatorId, Guid articleId)
        {
            UpdateArticleOperation(operatorId, articleId.ToString(), ArticleOperationSourceType.Article, ArticleOperationType.Like);
        }

        public void DislikeArticle(string operatorId, Guid articleId)
        {
            UpdateArticleOperation(operatorId, articleId.ToString(), ArticleOperationSourceType.Article, ArticleOperationType.Dislike);
        }

        public void LikeArticleComment(string operatorId, long commentId)
        {
            UpdateArticleOperation(operatorId, commentId.ToString(), ArticleOperationSourceType.Comment, ArticleOperationType.Like);
        }

        public void DislikeArticleComment(string operatorId, long commentId)
        {
            UpdateArticleOperation(operatorId, commentId.ToString(), ArticleOperationSourceType.Comment, ArticleOperationType.Dislike);
        }

        public Article Add(Article article)
        {
            throw new NotImplementedException();
        }

        public void ReportArticle(string operatorId, Article article)
        {
            throw new NotImplementedException();
        }

        public void ReportArticleComment(string operatorId, ArticleComment comment)
        {
            throw new NotImplementedException();
        }

        public void AdoptArticleComment(Article article, ArticleComment comment)
        {
            throw new NotImplementedException();
        }
    }
}
