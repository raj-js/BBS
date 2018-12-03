using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Infrastructure.Extensions;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class ArticleRepository : RepositoryBase, IArticleRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public IQueryable<Article> GetArticles()
        {
            return Context.Articles.AsQueryable();
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
    }
}
