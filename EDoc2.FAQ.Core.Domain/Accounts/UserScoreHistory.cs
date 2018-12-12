using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    /// <summary>
    /// 积分变化历史
    /// </summary>
    public class UserScoreHistory : Entity
    {
        public string UserId { get; set; }

        /// <summary>
        /// 分数变化原因
        /// </summary>
        public virtual UserScoreChangeReason Reason { get; set; }

        /// <summary>
        /// 初始分数
        /// </summary>
        public int OriginScore { get; set; }

        /// <summary>
        /// 变化值
        /// </summary>
        public int ChangeScore { get; set; }

        /// <summary>
        /// 最终分数
        /// </summary>
        public int FinalScore { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        public virtual User User { get; set; }
    }
}
