using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    /// <summary>
    /// 用户关注
    /// </summary>
    public class UserSubscriber : Entity
    {
        /// <summary>
        /// 粉丝编号
        /// </summary>
        public string FanId { get; set; }

        /// <summary>
        /// 被关注编号
        /// </summary>
        public string FollowId { get; set; }

        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }

        /// <summary>
        /// 关注者
        /// </summary>
        public virtual User Fan { get; set; }

        /// <summary>
        /// 被关注者
        /// </summary>
        public virtual User Follow { get; set; }
    }
}
