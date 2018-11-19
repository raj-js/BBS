using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public interface IArticleRepository : IRepository<Article>
    {
        /// <summary>
        /// 更新Article 状态
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Article Update(Article entity);

        /// <summary>
        /// 根绝编号查找文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Article> FindAsync(Guid id);

        /// <summary>
        /// 将文章保存为草稿
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        Article SaveAsDraft(Article article);

        /// <summary>
        /// 发布文章
        /// </summary>
        /// <param name="article"></param>
        /// <param name="auditing">是否需要审核</param>
        /// <param name="operatorId">操作人</param>
        /// <returns></returns>
        Article Release(string operatorId, bool auditing, Article article);

        /// <summary>
        /// 添加操作（赞/踩 文章/评论）
        /// </summary>
        /// <param name="operatorId">操作人</param>
        /// <param name="article"></param>
        /// <returns>赞数与踩数</returns>
        void AddOperation(string operatorId, string sourceId, ArticleOperationSourceType sourceType, ArticleOperationType operationType);

        /// <summary>
        /// 获取文章的属性
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        ICollection<ArticleProperty> GetArticleProperties(Article article);

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
        /// 回复文章
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="article"></param>
        /// <param name="comment"></param>
        void AddComment(string operatorId, Article article, ArticleComment comment);

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="article"></param>
        /// <param name="parentComment">被回复的评论</param>
        /// <param name="comment"></param>
        void AddComment(string operatorId, Article article, ArticleComment parentComment, ArticleComment comment);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="article"></param>
        /// <param name="comment"></param>
        void DeleteComment(string operatorId, Article article, ArticleComment comment);
    }
}
