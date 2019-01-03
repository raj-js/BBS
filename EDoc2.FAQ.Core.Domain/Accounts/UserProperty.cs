using System;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class UserProperty : Entity<Guid>
    {
        /// <summary>
        /// 持续签到天数
        /// </summary>
        public const string PersistDays = "PersistDays";

        /// <summary>
        /// 用户积分
        /// </summary>
        public const string Score = "Score";

        /// <summary>
        /// 粉丝数
        /// </summary>
        public const string Fans = "Fans";

        /// <summary>
        /// 关注数
        /// </summary>
        public const string Follows = "Follows";

        /// <summary>
        /// 头像
        /// </summary>
        public const string Avatar = "Avatar";

        /// <summary>
        /// 封面
        /// </summary>
        public const string Cover = "Cover";

        /// <summary>
        /// 所属用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public virtual User User { get; set; }

    }
}
