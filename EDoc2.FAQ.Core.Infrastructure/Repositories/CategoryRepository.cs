using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Categories;
using EDoc2.FAQ.Core.Domain.Repositories;
using EDoc2.FAQ.Core.Infrastructure.Extensions;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase, ICategoryRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public async Task Add(Category category)
        {
            await Context.Set<Category>().AddAsync(category);
        }

        public async Task Update(Category category, params string[] properties)
        {
            Context.AttachIfNot(category);
            Context.UpdatePartly(category, properties);
            await Task.CompletedTask;
        }

        public async Task Delete(Category category)
        {
            Context.Set<Category>().Remove(category);
            await Task.CompletedTask;
        }

        public IQueryable<Category> GetCategories()
        {
            return Context.Set<Category>();
        }

        public async Task<Category> FindCategoryById(Guid categoryId)
        {
            return await Context.Set<Category>().FindAsync(categoryId);
        }
    }
}
