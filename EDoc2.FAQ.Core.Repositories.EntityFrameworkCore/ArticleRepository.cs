using EDoc2.FAQ.Core.Domain.Models.ArticleAggregate;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
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

        public Article Add(Article entity)
        {
            return entity.Id != default(Guid) ? entity : _context.Articles.Add(entity).Entity;
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
    }
}
