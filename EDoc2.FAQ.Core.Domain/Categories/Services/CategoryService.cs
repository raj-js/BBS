using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Exceptions;
using EDoc2.FAQ.Core.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Categories.Services
{
    public class CategoryService : DomainService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo ?? throw new ArgumentNullException(nameof(categoryRepo));
        }

        public IQueryable<Category> GetRootCategories()
        {
            return _categoryRepo.GetCategories()
                .Where(s => s.ParentId == null);
        }

        public IQueryable<Category> GetSubCategories(Category category)
        {
            return category.Children.AsQueryable();
        }

        public async Task<Category> FindCategoryById(Guid id)
        {
            return await _categoryRepo.FindCategoryById(id);
        }

        public async Task AddCategory(User @operator, Category category)
        {
            if (!@operator.IsAdministrator)
                throw new UnauthorizedAccessException();

            var categories = GetRootCategories();
            if (categories.Any(s => s.Name.Equals(category.Name) && s.Enabled))
                throw new DuplicateException(category.Name);

            category.Enabled = true;
            await _categoryRepo.Add(category);
        }

        public async Task AddSubCategory(User @operator, Category parent, Category child)
        {
            if (!@operator.IsAdministrator && !@operator.IsModerator)
                throw new UnauthorizedAccessException();

            if(!parent.Enabled)
                throw new InvalidOperationException();

            if (parent.Children.Any(s => s.Name.Equals(child.Name) && s.Enabled))
                throw new DuplicateException(child.Name);

            child.ParentId = parent.Id;
            child.Enabled = true;
            await _categoryRepo.Add(child);
        }

        public async Task Enable(User @operator, Category category, bool enable = true)
        {
            if (category.ParentId == null && !@operator.IsAdministrator)
                throw new UnauthorizedAccessException();

            if (category.ParentId != null && !@operator.IsAdministrator && !@operator.IsModerator)
                throw new UnauthorizedAccessException();
            
            category.SetEnabled(enable);
            await _categoryRepo.Update(category, nameof(Category.Enabled));
        }

        public async Task AddCategoryModerator(User @operator, Category category, User moderator)
        {
            if (!@operator.IsAdministrator)
                throw new UnauthorizedAccessException();

            if (!category.Enabled)
                throw new InvalidOperationException();

            if(moderator.IsMuted)
                throw new InvalidOperationException();

            category.AddModerator(moderator.Id);
            await Task.CompletedTask;
        }

        public async Task RemoveCategoryModerator(User @operator, Category category, User moderator)
        {
            if (!@operator.IsAdministrator)
                throw new UnauthorizedAccessException();

            category.RemoveModerator(moderator.Id);
            await Task.CompletedTask;
        }

        public async Task AddCategoryArticle(Category category, Article article)
        {
            if (!category.Enabled)
                throw new InvalidOperationException();

            category.AddArticle(article.Id);
            await Task.CompletedTask;
        }
    }
}
