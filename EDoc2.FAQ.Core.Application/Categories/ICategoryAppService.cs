﻿using System;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Categories.Dtos.CategoryDtos;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;

namespace EDoc2.FAQ.Core.Application.Categories
{
    public interface ICategoryAppService : IAppService
    {
        Task<RespWapper> GetRootCategories();

        Task<RespWapper> GetSubCategories(Guid categoryId);

        Task<RespWapper> AddCategory(AddCategoryReq req);

        Task<RespWapper> AddSubCategory(AddCategoryReq req);

        Task<RespWapper> Enable(EnableReq req);
    }
}
