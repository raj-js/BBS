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
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> GetArticleTypes();

        /// <summary>
        /// 获取所有状态
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> GetArticleStates();

        /// <summary>
        /// 搜索文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> Search(SearchReq req);

        /// <summary>
        /// 访问文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> View(Guid id);

        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="req"></param>
        Task<RespWapper> AddQuestion(AddQuestionReq req);

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="req"></param>
        Task<RespWapper> AddArticle(AddArticleReq req);

        /// <summary>
        /// 编辑问题
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> EditQuestion(EditQuestionReq req);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> EditArticle(EditArticleReq req);

        /// <summary>
        /// 赞文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> LikeArticle(Guid id);

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> DislikeArticle(Guid id);

        /// <summary>
        /// 赞评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> LikeComment(long id);

        /// <summary>
        /// 踩评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> DislikeComment(long id);

        /// <summary>
        /// 举报文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> ReportArticle(ReportArticleReq req);

        /// <summary>
        /// 举报评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> ReportComment(ReportCommentReq req);

        /// <summary>
        /// 回复文章
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> ReplyArticle(ReplyArticleReq req);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> ReplyComment(ReplyCommentReq req);
    }
}
