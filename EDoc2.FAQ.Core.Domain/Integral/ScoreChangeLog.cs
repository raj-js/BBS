using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Integral
{
    /// <summary>
    /// 积分变化日志
    /// </summary>
    public class ScoreChangeLog : Entity
    {
        public string UserId { get; set; }

        /// <summary>
        /// 分数变化原因
        /// </summary>
        public ScoreChangeReason Reason { get; set; }

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
    }
}
