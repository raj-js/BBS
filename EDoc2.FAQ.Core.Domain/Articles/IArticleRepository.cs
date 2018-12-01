using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Repositories;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public interface IArticleRepository : IRepository<Article>
    {
        IQueryable<Article> GetArticles();
        Task Add(Article article);
        Task Update(Article article, params string[] properties);
        Task Delete(Article article);

        Task AddArticleProperty(ArticleProperty property);
        Task UpdateArticleProperty(ArticleProperty property, params string[] properties);

        Task AddComment(ArticleComment comment);
        Task UpdateComment(ArticleComment comment, params string[] properties);
        Task DeleteComment(ArticleComment comment);

        Task AddArticleOperation(ArticleOperation operation);
        Task UpdateArticleOperation(ArticleOperation operation, params string[] properties);
    }
}
