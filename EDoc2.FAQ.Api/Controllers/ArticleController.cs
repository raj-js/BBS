using EDoc2.FAQ.Api.Infrastructure;
using EDoc2.FAQ.Core.Application.Articles;
using EDoc2.FAQ.Core.Application.DtoBase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using static EDoc2.FAQ.Core.Application.Articles.Dtos.ArticleDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [JwtAuthorize]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Guid>))]
        public async Task<IActionResult> AddArticle([FromBody]AddArticleReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _articleAppService.AddArticle(req));
        }

        /// <summary>
        /// 创建问题
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("addQuestion")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Guid>))]
        public async Task<IActionResult> AddQuestion([FromBody]AddQuestionReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _articleAppService.AddQuestion(req));
        }

        /// <summary>
        /// 查看文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<ArticleResp>))]
        [AllowAnonymous]
        public async Task<IActionResult> View([FromQuery]Guid id)
        {
            return Ok(await _articleAppService.View(id));
        }

        /// <summary>
        /// 加载评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("getComments")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<CommentItem>>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetComments([FromQuery]LoadCommentsReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.GetComments(req));
        }

        /// <summary>
        /// 存为草稿
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("addDraft")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Guid>))]
        public async Task<IActionResult> AddDraft([FromBody]AddArticleReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.AddDraft(req));
        }

        /// <summary>
        /// 编辑问题
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("editQuestion")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Guid>))]
        public async Task<IActionResult> EditQuestion([FromBody]EditQuestionReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.EditQuestion(req));
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("editArticle")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Guid>))]
        public async Task<IActionResult> EditArticle([FromBody]EditArticleReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.EditArticle(req));
        }

        /// <summary>
        /// 修改文章是否可以回复评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("editCanComment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Guid>))]
        public async Task<IActionResult> EditCanComment([FromBody]EditCanCommentReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.EditCanComment(req));
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> Delete([FromQuery]Guid id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.Delete(id));
        }

        /// <summary>
        /// 强制删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("forced")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> DeleteForced([FromQuery]Guid id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.DeleteForced(id));
        }

        /// <summary>
        /// 置顶文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("top")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> TopArticle([FromBody]TopArticleReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.TopArticle(req));
        }

        /// <summary>
        /// 取消置顶文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("cancelTop")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> CancelTopArticle([FromBody]Guid id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.CancelTopArticle(id));
        }

        /// <summary>
        /// 赞文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("like")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<LikeOrNotResp>))]
        public async Task<IActionResult> Like([FromBody]Guid id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.LikeArticle(id));
        }

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("dislike")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<LikeOrNotResp>))]
        public async Task<IActionResult> Dislike([FromBody]Guid id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.DislikeArticle(id));
        }

        /// <summary>
        /// 赞评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("likeComment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<LikeOrNotResp>))]
        public async Task<IActionResult> LikeComment([FromBody]long id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.LikeComment(id));
        }

        /// <summary>
        /// 赞评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("dislikeComment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<LikeOrNotResp>))]
        public async Task<IActionResult> DisLikeComment([FromBody]long id)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.DislikeComment(id));
        }

        /// <summary>
        /// 回复文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("reply")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<CommentItem>))]
        public async Task<IActionResult> Reply([FromBody]ReplyArticleReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.ReplyArticle(req));
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("replyComment")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<CommentItem>))]
        public async Task<IActionResult> ReplyComment([FromBody]ReplyCommentReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.ReplyComment(req));
        }

        /// <summary>
        /// 结帖
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("finish")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<ArticleResp>))]
        public async Task<IActionResult> Finish([FromBody]FinishReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.Finish(req));
        }

        /// <summary>
        /// 获取用户文章/问题
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("getUserArticles")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<ListItem>>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserArticles([FromQuery]UserArticlesReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.GetUserArticles(req));
        }

        /// <summary>
        /// 获取用户收藏
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("getUserFavorites")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<ListItem>>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserFavorites([FromQuery]UserFavoritesReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _articleAppService.GetUserFavorites(req));
        }
    }
}