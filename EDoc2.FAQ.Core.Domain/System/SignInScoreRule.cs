using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.System
{
    /// <summary>
    /// 签到积分规则
    /// </summary>
    public class SignInScoreRule : Entity
    {
        /// <summary>
        /// 签到天数大于
        /// </summary>
        public int? MoreThan { get; set; }

        /// <summary>
        /// 签到天数小于
        /// </summary>
        public int? LessThan { get; set; }

        /// <summary>
        /// 签到获取积分
        /// </summary>
        public int Score { get; set; }
    }
}
