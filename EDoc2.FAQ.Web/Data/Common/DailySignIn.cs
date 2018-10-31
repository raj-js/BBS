using EDoc2.FAQ.Web.Data.Identity;
using System;

namespace EDoc2.FAQ.Web.Data.Common
{
    public class DailySignIn
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClientIp { get; set; }

        /// <summary>
        /// 打卡时间 (包含时分秒)
        /// </summary>
        public DateTime SignInTime { get; set; }

        public virtual AppUser User { get; set; }
    }
}
