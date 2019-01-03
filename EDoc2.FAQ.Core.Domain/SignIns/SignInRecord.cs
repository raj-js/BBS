using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.SignIns
{
    /// <summary>
    /// 签到记录
    /// </summary>
    public class SignInRecord : Entity, IAggregateRoot
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 持续签到天数
        /// </summary>
        public int PersistDays { get; set; }

        /// <summary>
        /// 获得积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime SignInTime { get; set; }
    }
}
