using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.SignIns
{
    /// <summary>
    /// 签到积分规则
    /// </summary>
    public class SignInRule : Entity
    {
        /// <summary>
        /// 签到天数大于
        /// </summary>
        public int Minimum { get; private set; }

        /// <summary>
        /// 签到天数小于
        /// </summary>
        public int? Maxmum { get; private set; }

        /// <summary>
        /// 签到获取积分
        /// </summary>
        public int GetScore { get; private set; }

        public SignInRule(int minimum, int? maxmum, int getScore)
        {
            if(minimum < 0)
                throw new ArgumentOutOfRangeException(nameof(minimum));

            Minimum = minimum;
            Maxmum = maxmum;
            GetScore = getScore;
        }
    }
}
