using System;
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
        /// 根据编号获取文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<Article> FindById(Guid articleId);

        /// <summary>
        /// 查看文章
        /// </summary>
        /// <param name="article"></param>
        /// <param name="user">当前用户</param>
        /// <param name="viewInterval">访问控制（分）</param>
        /// <returns></returns>
        Task View(Article article, User user, int viewInterval);

        /// <summary>
        /// 游客查看文章
        /// </summary>
        /// <param name="article"></param>
        /// <param name="clientIp"></param>
        /// <param name="viewInterval">访问控制（分）</param>
        /// <returns></returns>
        Task View(Article article, string clientIp, int viewInterval);

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
        /// <param name="author"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        Task Create(User author, Article article);

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Task Edit(Article article);

        /// <summary>
        /// 发布文章
        /// </summary>
        /// <param name="author"></param>
        /// <param name="article"></param>
        /// <param name="approve"></param>
        /// <returns></returns>
        Task Release(User author, Article article, bool approve = false);

        /// <summary>
        /// 发布问题
        /// </summary>
        /// <param name="author"></param>
        /// <param name="article"></param>
        /// <param name="score">悬赏分</param>
        /// <param name="approve"></param>
        /// <returns></returns>
        Task Release(User author, Article article, int score, bool approve = false);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <param name="isSoftDelete">是否软删除</param>
        /// <returns></returns>
        Task Delete(User @operator, Article article, bool isSoftDelete = true);

        /// <summary>
        /// 结贴
        /// </summary>
        /// <param name="article">文章</param>
        /// <param name="adoptComment">最佳评论；不满意结贴，此参数为NULL</param>
        /// <param name="unsatisfactory">不满意结贴</param>
        /// <returns></returns>
        Task Finish(Article article, ArticleComment adoptComment = null, bool unsatisfactory = true);

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
        /// <param name="comment"></param>
        /// <returns></returns>
        Task Like(User @operator, ArticleComment comment);

        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task Dislike(User @operator, ArticleComment comment);

        /// <summary>
        /// 回复文章
        /// </summary>
        /// <param name="operator">回复人</param>
        /// <param name="article">被回复的文章</param>
        /// <param name="replyComment">回复的内容</param>
        /// <param name="approve"></param>
        /// <returns></returns>
        Task Reply(User @operator, Article article, ArticleComment replyComment, bool approve = false);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="operator">回复人</param>
        /// <param name="article">文章</param>
        /// <param name="comment">被回复的评论</param>
        /// <param name="replyComment">回复的内容</param>
        /// <param name="approve"></param>
        /// <returns></returns>
        Task Reply(User @operator, Article article, ArticleComment comment, ArticleComment replyComment,
            bool approve = false);

        /// <summary>
        /// 文章置顶
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <param name="isForever"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        Task Top(User @operator, Article article, bool isForever, DateTime? expirationTime = null);

        /// <summary>
        /// 取消文章置顶
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        Task CancelTop(User @operator, Article article);

        #endregion
    }
}
