using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Services;

namespace EDoc2.FAQ.Core.Domain.Articles.Services
{
    public class ArticleService : DomainService, IArticleService
    {
        private readonly IArticleRepository _articleRepo;

        public ArticleService(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo ?? throw new ArgumentNullException(nameof(articleRepo));
        }

        public IQueryable<Article> GetArticles()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Article> View(Article article, User user = null, string clientIp = null)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<ArticleComment> GetComments(Article article)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Article> Create(Article article)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Article> Edit(Article article)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Article> Release(Article article)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Delete(Article article, bool isSoftDelete = true)
        {
            throw new NotImplementedException();
        }

        public async Task Like(User @operator, Article article)
        {
            throw new System.NotImplementedException();
        }

        public async Task Dislike(User @operator, Article article)
        {
            throw new System.NotImplementedException();
        }

        public async Task Like(User @operator, Article article, ArticleComment comment)
        {
            throw new System.NotImplementedException();
        }

        public async Task Dislike(User @operator, Article article, ArticleComment comment)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ArticleComment> Reply(User @operator, Article article, ArticleComment replyComment)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ArticleComment> Reply(User @operator, Article article, ArticleComment comment, ArticleComment replyComment)
        {
            throw new System.NotImplementedException();
        }
    }
}
