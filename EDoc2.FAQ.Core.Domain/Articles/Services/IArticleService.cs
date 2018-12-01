using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Services;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Articles.Services
{
    public interface IArticleService : IDomainService
    {
        #region 查询

        /// <summary>
        /// 获取文章
        /// </summary>
        /// <returns></returns>
        IQueryable<Article> GetArticles();

        /// <summary>
        /// 查看文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="user">当前用户</param>
        /// <param name="clientIp">客户端ip</param>
        /// <returns></returns>
        Task<Article> View(Article article, User user = null, string clientIp = null);

        /// <summary>
        /// 获取文章的评论
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        IQueryable<ArticleComment> GetComments(Article article);

        #endregion


        #region 命令

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task<Article> Create(Article article);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task<Article> Edit(Article article);

        /// <summary>
        /// 发布文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task<Article> Release(Article article);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="article"></param>
        /// <param name="isSoftDelete">是否软删除</param>
        /// <returns></returns>
        Task<Article> Delete(Article article, bool isSoftDelete = true);

        /// <summary>
        /// 赞文章
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        Task Like(User @operator, Article article);

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        Task Dislike(User @operator, Article article);

        /// <summary>
        /// 赞评论
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task Like(User @operator, Article article, ArticleComment comment);

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task Dislike(User @operator, Article article, ArticleComment comment);

        /// <summary>
        /// 回复文章
        /// </summary>
        /// <param name="operator">回复人</param>
        /// <param name="article">被回复的文章</param>
        /// <param name="replyComment">回复的内容</param>
        /// <returns></returns>
        Task<ArticleComment> Reply(User @operator, Article article, ArticleComment replyComment);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="operator">回复人</param>
        /// <param name="article">文章</param>
        /// <param name="comment">被回复的评论</param>
        /// <param name="replyComment">回复的内容</param>
        /// <returns></returns>
        Task<ArticleComment> Reply(User @operator, Article article, ArticleComment comment, ArticleComment replyComment);

        #endregion
    }
}
