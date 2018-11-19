﻿using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Integral
{
    /// <summary>
    /// 签到记录
    /// </summary>
    public class SignInLog : Entity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 持续签到天数
        /// </summary>
        public int PersistSignInDays { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime SignInTime { get; set; }
    }
}
