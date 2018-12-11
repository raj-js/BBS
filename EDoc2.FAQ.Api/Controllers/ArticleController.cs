using EDoc2.FAQ.Core.Application.Articles;
using EDoc2.FAQ.Core.Application.DtoBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Articles.Dtos.ArticleDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleAppService _articleAppService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="articleAppService"></param>
        public ArticleController(IArticleAppService articleAppService)
        {
            _articleAppService = articleAppService ?? throw new ArgumentNullException(nameof(articleAppService));
        }

        /// <summary>
        /// 获取所有文章类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("types")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<List<ValueTitlePair<int>>>))]
        public async Task<IActionResult> GetTypes()
        {
            var response = await _articleAppService.GetArticleTypes();
            return Ok(response);
        }

        /// <summary>
        /// 获取所有文章状态
        /// </summary>
        /// <returns></returns>
        [HttpGet("states")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<List<ValueTitlePair<int>>>))]
        public async Task<IActionResult> GetStates()
        {
            var response = await _articleAppService.GetArticleStates();
            return Ok(response);
        }

        /// <summary>
        /// 搜索文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<ListItem>>))]
        public async Task<IActionResult> Search([FromQuery]SearchReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            var response = await _articleAppService.Search(req);
            return Ok(response);
        }

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("addArticle")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddArticle([FromBody]AddArticleReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            return null;
        }

        /// <summary>
        /// 创建问题
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("addQuestion")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddQuestion([FromBody]AddQuestionReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            return null;
        }



    }
}