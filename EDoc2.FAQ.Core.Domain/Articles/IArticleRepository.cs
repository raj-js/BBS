using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public interface IArticleRepository : IRepository<Article>
    {
        #region 文章相关
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Article Add(Article article);
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Article Update(Article article);
        /// <summary>
        /// 根据编号查找文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Article Find(Guid id);
        /// <summary>
        /// 根绝编号查找文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Article> FindAsync(Guid id);
        /// <summary>
        /// 发布文章
        /// </summary>
        /// <param name="article"></param>
        /// <param name="auditing">是否需要审核</param>
        /// <param name="operatorId">操作人</param>
        /// <returns></returns>
        Article Release(string operatorId, bool auditing, Article article);
        /// <summary>
        /// 赞文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="articleId"></param>
        void LikeArticle(string operatorId, Guid articleId);
        /// <summary>
        /// 踩文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="articleId"></param>
        void DislikeArticle(string operatorId, Guid articleId);
        /// <summary>
        /// 举报文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="article"></param>
        void ReportArticle(string operatorId, Article article);
        /// <summary>
        /// 访问文章
        /// </summary>
        /// <param name="article"></param>
        /// <param name="viewTime">访问时间</param>
        /// <param name="clientIp">客户端IP</param>
        /// <param name="operatorId">操作人（游客为NULL）</param>
        /// <returns></returns>
        Article ViewArticle(Article article, DateTime viewTime, string clientIp, string operatorId = null);
        /// <summary>
        /// 文章能否收藏
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task<bool> CanFavoriteArticle(Guid articleId);
        #endregion

        #region 评论相关
        /// <summary>
        /// 赞评论
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="commentId"></param>
        void LikeArticleComment(string operatorId, long commentId);
        /// <summary>
        /// 踩评论
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="commentId"></param>
        void DislikeArticleComment(string operatorId, long commentId);
        /// <summary>
        /// 举报评论
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="comment"></param>
        void ReportArticleComment(string operatorId, ArticleComment comment);
        /// <summary>
        /// 回复文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="article"></param>
        /// <param name="comment"></param>
        void ReplyArticle(Guid articleId, ArticleComment comment, bool auditing);
        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="article"></param>
        /// <param name="parentComment">被回复的评论</param>
        /// <param name="comment"></param>
        void ReplyArticleComment(long parentCommentId, ArticleComment comment, bool auditing);
        /// <summary>
        /// 采纳最佳评论
        /// </summary>
        /// <param name="article"></param>
        /// <param name="comment"></param>
        void AdoptArticleComment(Article article, ArticleComment comment);
        #endregion
    }
}
