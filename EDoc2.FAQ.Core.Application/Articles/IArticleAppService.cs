using EDoc2.FAQ.Core.Application.Articles.Dtos;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public interface IArticleAppService : IAppService
    {
        /// <summary>
        /// 搜索文章
        /// </summary>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<PagingDto<ArticleDtos.ListItem>> SearchAsync(ArticleDtos.Search searchDto);

        /// <summary>
        /// 使用搜索引擎搜索文章
        /// </summary>
        /// <returns></returns>
        Task<PagingDto<ArticleDtos.ListItem>> SearchByEngineAsync(ArticleDtos.SearchByES searchByESDto);

        /// <summary>
        /// 根据编号获取文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<ArticleDtos.Details> FindAsync(Guid articleId);

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="addDto"></param>
        ArticleDtos.Details AddArticle(ArticleDtos.Add addDto);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="editDto"></param>
        /// <returns></returns>
        ArticleDtos.Details EditArticle(ArticleDtos.Edit editDto);

        /// <summary>
        /// 访问文章，增加访问量
        /// </summary>
        /// <param name="articleId">文章编号</param>
        /// <param name="clentIp">客户端Ip</param>
        /// <param name="operatorId">
        /// 访问人编号
        /// NULL 表示游客
        /// </param>
        /// <returns></returns>
        Task ViewArticle(Guid articleId, string clentIp, string operatorId = null);

        /// <summary>
        /// 赞文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task LikeArticle(string operatorId, Guid articleId);

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task DislikeArticle(string operatorId, Guid articleId);

        /// <summary>
        /// 举报文章
        /// </summary>
        /// <param name="operatorId">举报人编号</param>
        /// <param name="articleId">文章编号</param>
        /// <param name="reason">举报原因</param>
        /// <returns></returns>
        Task ReportArticle(string operatorId, Guid articleId, string reason);
    }
}
