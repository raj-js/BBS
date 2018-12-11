using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Repositories;

namespace EDoc2.FAQ.Core.Domain.Categories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task Add(Category category);
        Task Update(Category category, params string[] properties);
        Task Delete(Category category);
        IQueryable<Category> GetCategories();
        Task<Category> FindCategoryById(Guid categoryId);
    }
}
