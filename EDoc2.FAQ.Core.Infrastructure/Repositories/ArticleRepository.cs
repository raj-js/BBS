using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class ArticleRepository : RepositoryBase, IArticleRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public Article Update(Article entity)
        {
            return Context.Articles.Update(entity).Entity;
        }

        public Article Find(Guid id)
        {
            return Context.Articles.Find(id);
        }

        public async Task<Article> FindAsync(Guid key)
        {
            return await Context.Articles.FindAsync(key);
        }

        public Article Release(string operatorId, bool auditing, Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            if (auditing)
                article.SetAuditing(operatorId);
            else
                article.SetPublished(operatorId);

            if (article.IsTransient())
                return Context.Articles.Add(article).Entity;

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
            var op = Context.ArticleOperations
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
                Context.ArticleOperations.Add(op);
            }
            else
            {
                op.IsCancel = false;
                op.OperationTime = DateTime.Now;
                Context.ArticleOperations.Update(op);
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

        public async Task<bool> CanFavoriteArticle(Guid articleId)
        {
            var article = await FindAsync(articleId);

            if (article == null) return false;

            return new int[]
            {
                ArticleState.Published.Id,
                ArticleState.Solved.Id,
                ArticleState.UnSolved.Id,
                ArticleState.Unsatisfactory.Id
            }.Contains(article.State.Id);
        }
    }
}
