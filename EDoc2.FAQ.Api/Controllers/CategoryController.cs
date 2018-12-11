using EDoc2.FAQ.Core.Application.Categories;
using EDoc2.FAQ.Core.Application.DtoBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Categories.Dtos.CategoryDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 类别
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _categoryAppService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="categoryAppService"></param>
        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService ?? throw new ArgumentNullException(nameof(categoryAppService));
        }

        /// <summary>
        /// 获取分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<List<CategoryResp>>))]
        public async Task<IActionResult> GetCategories([FromQuery]Guid? id)
        {
            return id == null ? 
                Ok(await _categoryAppService.GetRootCategories()) : 
                Ok(await _categoryAppService.GetSubCategories(id.Value));
        }

        /// <summary>
        /// 增加类别
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<CategoryResp>))]
        public async Task<IActionResult> AddCategory([FromBody]AddCategoryReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            return req.ParentId == null ?
                Ok(await _categoryAppService.AddCategory(req)) :
                Ok(await _categoryAppService.AddSubCategory(req));
        }

        /// <summary>
        /// 启用/禁用模块
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<CategoryResp>))]
        public async Task<IActionResult> Enable([FromBody]EnableReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _categoryAppService.Enable(req));
        }
    }
}