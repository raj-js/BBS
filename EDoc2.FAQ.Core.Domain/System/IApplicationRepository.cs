using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.System
{
    public interface IApplicationRepository: IRepository<Application, Guid>
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
    }
}
