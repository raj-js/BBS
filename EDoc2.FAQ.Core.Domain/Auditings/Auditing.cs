using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Auditings
{
    /// <summary>
    /// 审核记录
    /// </summary>
    public class Auditing : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 关联类型
        /// </summary>
        public AuditingTargetType TargetType { get; set; }

        /// <summary>
        /// 关联事物编号
        /// </summary>
        public string TargetId { get; set; }

        /// <summary>
        /// 审核结果
        /// </summary>
        public AuditingResult Result { get; private set; }

        /// <summary>
        /// 审核人编号
        /// </summary>
        public string AuditorId { get; private set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime AuditingTime { get; private set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; private set; }

        #region 审核结果

        public void SetPassed()
        {

        }

        #endregion

    }
}
