using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Integral;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly CommunityContext _context;
        private readonly IScoreRepository _scoreRepository;

        public IUnitOfWork UnitOfWork => _context;

        public ArticleRepository(CommunityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Article Update(Article entity)
        {
            return _context.Articles.Update(entity).Entity;
        }

        public async Task<Article> FindAsync(Guid key)
        {
            return await _context.Articles.FindAsync(key);
        }

        public void Delete(Article entity)
        {
            _context.Articles.Remove(entity);
        }

        public Article SaveAsDraft(Article article)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            article.SetDraft();

            if (article.IsTransient())
                return _context.Articles.Add(article).Entity;
            else
                return Update(article);
        }

        public Article Release(string operatorId, bool auditing, Article article)
        {
            if (article == null) throw new ArgumentNullException(nameof(article));

            if (!article.HasSpentScore())
            {
                var rewardScore = article.GetRewardScore();
                if (!_scoreRepository.HasEnoughScore(article.CreatorId, rewardScore))
                    throw new InvalidOperationException("积分不足");
            }

            if (auditing)
                article.SetAuditing(operatorId);
            else
                article.SetPublished(operatorId);

            if (article.IsTransient())
                return _context.Articles.Add(article).Entity;
            else
                return Update(article);
        }

        public void AddOperation(string operatorId, string sourceId, ArticleOperationSourceType sourceType, ArticleOperationType operationType)
        {
            var operation = _context.ArticleOperations.SingleOrDefault(o => o.OperatorId == operatorId && o.SourceId == o.SourceId && o.SourceType.Id == sourceType.Id);
            if (operation == null)
            {
                operation = new ArticleOperation
                {
                    OperatorId = operatorId,
                    SourceId = sourceId,
                    SourceType = sourceType,
                    Type = operationType,
                    OperationTime = DateTime.Now
                };
                _context.ArticleOperations.Add(operation);
            }
            else
            {
                operation.Type = operationType;
                operation.OperationTime = DateTime.Now;
                _context.ArticleOperations.Update(operation);
            }

            //更新点赞/踩数
        }

        public ICollection<ArticleProperty> GetArticleProperties(Article article)
        {
            throw new NotImplementedException();
        }

        public Article ViewArticle(Article article, DateTime viewTime, string clientIp, string operatorId = null)
        {
            throw new NotImplementedException();
        }

        public void AddComment(string operatorId, Article article, ArticleComment comment)
        {
            throw new NotImplementedException();
        }

        public void AddComment(string operatorId, Article article, ArticleComment parentComment, ArticleComment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(string operatorId, Article article, ArticleComment comment)
        {
            throw new NotImplementedException();
        }
    }
}
