using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Categories.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Categories.Dtos.CategoryDtos;

namespace EDoc2.FAQ.Core.Application.Categories
{
    public class CategoryAppService : AppServiceBase, ICategoryAppService
    {
        private readonly ICategoryService _categoryService;

        public CategoryAppService(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public async Task<RespWapper> GetRootCategories()
        {
            var categories = _categoryService.GetRootCategories()
                .WhereFalse(CurrentUser != null && CurrentUser.IsAdministrator, s => s.Enabled);

            await Task.CompletedTask;
            return RespWapper.Successed(categories.Select(s => CategoryResp.From(s)).ToList());
        }

        public async Task<RespWapper> GetSubCategories(Guid categoryId)
        {
            var category = await _categoryService.FindCategoryById(categoryId);

            var categories = _categoryService.GetSubCategories(category)
                .WhereFalse(CurrentUser != null && (CurrentUser.IsAdministrator || CurrentUser.IsModerator), s => s.Enabled);

            await Task.CompletedTask;
            return RespWapper.Successed(categories.Select(s=> CategoryResp.From(s)).ToList());
        }

        public async Task<RespWapper> AddCategory(AddCategoryReq req)
        {
            var category = req.To();
            await _categoryService.AddCategory(CurrentUser, category);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(CategoryResp.From(category));
        }

        public async Task<RespWapper> AddSubCategory(AddCategoryReq req)
        {
            if(req.ParentId == null)
                return RespWapper.Failed();

            var parent = await _categoryService.FindCategoryById(req.ParentId.Value);

            var category = req.To();
            await _categoryService.AddSubCategory(CurrentUser, parent, category);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(CategoryResp.From(category));
        }

        public async Task<RespWapper> Enable(EnableReq req)
        {
            var category = await _categoryService.FindCategoryById(req.Id);
            if(category == null)
                return RespWapper.Failed();

            await _categoryService.Enable(CurrentUser, category, req.Enable);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(CategoryResp.From(category));
        }
    }
}
