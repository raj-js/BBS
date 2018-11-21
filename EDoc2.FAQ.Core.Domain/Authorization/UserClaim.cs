using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public class UserClaim : IdentityUserClaim<string>
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public const string Nickname = "Nickname";

        /// <summary>
        /// 加入日期
        /// </summary>
        public const string JoinDate = "JoinDate";

        /// <summary>
        /// 性别
        /// </summary>
        public const string Gender = "Gender";

        /// <summary>
        /// 所在城市
        /// </summary>
        public const string City = "City";

        /// <summary>
        /// 个性签名
        /// </summary>
        public const string Signature = "Signature";

        /// <summary>
        /// 持续签到天数
        /// </summary>
        public const string PersistSignInDays = "PersistSignInDays";

        /// <summary>
        /// 用户积分
        /// </summary>
        public const string Score = "Score";

        public virtual User User { get; set; }
    }
}
