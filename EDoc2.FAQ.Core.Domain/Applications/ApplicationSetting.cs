using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    /// <summary>
    /// 应用程序设置
    /// </summary>
    public class ApplicationSetting : Entity
    {
        #region 设置项

        /// <summary>
        /// 文章是否需要审核
        /// </summary>
        public const string IsArticleAuditing = "IsArticleAuditing";

        /// <summary>
        /// 回复是否需要审核
        /// </summary>
        public const string IsCommentAuditing = "IsCommentAuditing";

        /// <summary>
        /// 操作时间间隔（秒）
        /// </summary>
        public const string OperationInterval = "OperationInterval";

        /// <summary>
        /// 缓存过期时间 （分钟）
        /// </summary>
        public const string CacheExpireInterval = "CacheExpireInterval";

        /// <summary>
        /// 单用户/单Ip 查看文章， 增长文章访问量的间隔时长 （分钟）
        /// </summary>
        public const string ViewInterval = "ViewInterval";

        #endregion

        /// <summary>
        /// 设置项
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public Guid ApplicationId { get; set; }

        public virtual Application Application { get; set; }
    }
}
