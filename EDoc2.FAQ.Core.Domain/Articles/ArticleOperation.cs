using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    /// <summary>
    /// 文章操作
    /// </summary>
    public class ArticleOperation : Entity
    {
        /// <summary>
        /// 操作源
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 操作源类型
        /// </summary>
        public virtual ArticleOperationOperatorType OperatorType { get; set; }

        /// <summary>
        /// 操作目标
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 操作目标类型
        /// </summary>
        public virtual ArticleOperationTargetType TargetType  { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public virtual ArticleOperationType Type { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 是否取消，避免删除数据后无法追踪上一次操作时间
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
