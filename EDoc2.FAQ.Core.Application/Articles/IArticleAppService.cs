using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Articles.Dtos.ArticleDtos;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public interface IArticleAppService : IAppService
    {
        /// <summary>
        /// 搜索文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> Search(SearchReq req);

        /// <summary>
        /// 访问文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response> View(Guid id);

        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="req"></param>
        Task<Response> AddQuestion(AddQuestionReq req);

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="req"></param>
        Task<Response> AddArticle(AddArticleReq req);

        /// <summary>
        /// 编辑问题
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> EditQuestion(EditQuestionReq req);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> EditArticle(EditArticleReq req);

        /// <summary>
        /// 赞文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response> LikeArticle(Guid id);

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response> DislikeArticle(Guid id);

        /// <summary>
        /// 赞评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response> LikeComment(long id);

        /// <summary>
        /// 踩评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Response> DislikeComment(long id);

        /// <summary>
        /// 举报文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> ReportArticle(ReportArticleReq req);

        /// <summary>
        /// 举报评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> ReportComment(ReportCommentReq req);

        /// <summary>
        /// 回复文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> ReplyArticle(ReplyArticleReq req);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> ReplyComment(ReplyCommentReq req);
    }
}
