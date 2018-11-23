using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    /// <summary>
    /// 用户关注
    /// </summary>
    public class UserFollow : Entity
    {
        /// <summary>
        /// 追随者编号
        /// </summary>
        public string FollowerId { get; set; }

        /// <summary>
        /// 被关注者编号
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime FollowTime { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
