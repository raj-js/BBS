using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.System
{
    public interface IApplicationRepository: IRepository<Application>
    {
        /// <summary>
        /// 文章是否需要审核
        /// </summary>
        /// <returns></returns>
        bool IsArticleNeedAuditing();

        /// <summary>
        /// 评论是否需要审核
        /// </summary>
        /// <returns></returns>
        bool IsCommentNeedAuditing();

        /// <summary>
        /// 获取文章/回复 踩/赞 间隔时间（秒）
        /// </summary>
        /// <returns></returns>
        int GetArticleOperationInterval();

        /// <summary>
        /// 获取缓存过期间隔
        /// </summary>
        /// <returns></returns>
        TimeSpan GetCacheExpireTimespan();
    }
}
