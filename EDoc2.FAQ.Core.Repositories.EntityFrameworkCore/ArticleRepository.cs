using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Integral;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
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

            var rewardScore = article.GetRewardScore();
            if (_scoreRepository.HasEnoughScore(article.CreatorId, rewardScore))

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
            
        }

        public void TreadArticle(string operatorId, Article article)
        {
            throw new NotImplementedException();
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

        public void PraiseComment(string operatorId, Article article, ArticleComment comment)
        {
            throw new NotImplementedException();
        }

        public void TreadComment(string operatorId, Article article, ArticleComment comment)
        {
            throw new NotImplementedException();
        }
    }
}
