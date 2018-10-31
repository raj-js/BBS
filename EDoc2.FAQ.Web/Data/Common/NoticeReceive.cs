using System;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Data.Common
{
    /// <summary>
    /// 消息接收记录
    /// </summary>
    public class NoticeReceive
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public string NoticeId { get; set; }

        /// <summary>
        /// 接收者编号
        /// </summary>
        public string ReveiverId { get; set; }

        public NoticeReadState State { get; set; }

        public DateTime? ReadDate { get; set; }

        public virtual AppUser Receiver { get; set; }
        public virtual Notice Notice { get; set; }
    }
}
