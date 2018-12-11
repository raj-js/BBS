using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using ArticleOperation = EDoc2.FAQ.Core.Domain.Articles.ArticleOperation;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class ArticleRepository : RepositoryBase, IArticleRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public IQueryable<Article> GetArticles()
        {
            return Context.Set<Article>();
        }

        public async Task<Article> FindById(Guid id)
        {
            return await Context.FindAsync<Article>(id);
        }

        public async Task Add(Article article)
        {
            await Context.AddAsync(article);
        }

        public async Task Update(Article article, params string[] properties)
        {
            Context.AttachIfNot(article);
            Context.UpdatePartly(article, properties);
            await Task.CompletedTask;
        }

        public async Task Delete(Article article)
        {
            Context.AttachIfNot(article);
            Context.Remove(article);
            await Task.CompletedTask;
        }

        public async Task AddArticleProperty(ArticleProperty property)
        {
            await Context.AddAsync(property);
        }

        public async Task UpdateArticleProperty(ArticleProperty property, params string[] properties)
        {
            Context.AttachIfNot(property);
            Context.UpdatePartly(property, properties);
            await Task.CompletedTask;
        }

        public IQueryable<ArticleComment> GetArticleComments()
        {
            return Context.Set<ArticleComment>();
        }

        public async Task AddComment(ArticleComment comment)
        {
            await Context.AddAsync(comment);
        }

        public async Task UpdateComment(ArticleComment comment, params string[] properties)
        {
            Context.AttachIfNot(comment);
            Context.UpdatePartly(comment, properties);
            await Task.CompletedTask;
        }

        public async Task DeleteComment(ArticleComment comment)
        {
            Context.AttachIfNot(comment);
            Context.Remove(comment);
            await Task.CompletedTask;
        }

        public async Task AddArticleOperation(ArticleOperation operation)
        {
            await Context.AddAsync(operation);
        }

        public async Task UpdateArticleOperation(ArticleOperation operation, params string[] properties)
        {
            Context.AttachIfNot(operation);
            Context.UpdatePartly(operation, properties);
            await Task.CompletedTask;
        }

        public IQueryable<ArticleTop> GetArticleTops()
        {
            return Context.Set<ArticleTop>();
        }

        public async Task AddArticleTop(ArticleTop top)
        {
            await Context.Set<ArticleTop>().AddAsync(top);
        }

        public async Task UpdateArticleTop(ArticleTop top, params string[] properties)
        {
            Context.AttachIfNot(top);
            Context.UpdatePartly(top, properties);
            await Task.CompletedTask;
        }

        public IQueryable<ArticleOperation> GetOperations()
        {
            return Context.Set<ArticleOperation>();
        }

        public async Task AddOperation(ArticleOperation operation)
        {
            await Context.Set<ArticleOperation>().AddAsync(operation);
        }

        public async Task UpdateOperation(ArticleOperation operation, params string[] properties)
        {
            Context.AttachIfNot(operation);
            Context.UpdatePartly(operation, properties);
            await Task.CompletedTask;
        }
    }
}
