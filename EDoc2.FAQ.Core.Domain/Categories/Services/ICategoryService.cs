﻿using EDoc2.FAQ.Core.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Exceptions;

namespace EDoc2.FAQ.Core.Domain.Categories.Services
{
    public interface ICategoryService : IDomainService
    {
        #region 查询

        IQueryable<Category> GetRootCategories();
        IQueryable<Category> GetSubCategories(Category category);
        Task<Category> FindCategoryById(Guid id);

        #endregion

        #region 命令

        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="category"></param>
        /// <exception cref="DuplicateException"></exception>
        /// <returns></returns>
        Task AddCategory(User @operator, Category category);

        /// <summary>
        /// 添加子类别
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <exception cref="DuplicateException"></exception>
        /// <returns></returns>
        Task AddSubCategory(User @operator, Category parent, Category child);

        /// <summary>
        /// 启用/禁用 类别
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="category"></param>
        /// <param name="enable"></param>
        /// <returns></returns>
        Task Enable(User @operator, Category category, bool enable = true);

        /// <summary>
        /// 添加类别管理员
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="category"></param>
        /// <param name="moderator"></param>
        /// <returns></returns>
        Task AddCategoryModerator(User @operator, Category category, User moderator);

        /// <summary>
        /// 移除类别管理员
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="category"></param>
        /// <param name="moderator"></param>
        /// <returns></returns>
        Task RemoveCategoryModerator(User @operator, Category category, User moderator);

        /// <summary>
        /// 添加文章到指定类别
        /// </summary>
        /// <param name="category"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        Task AddCategoryArticle(Category category, Article article);

        #endregion
    }
}
