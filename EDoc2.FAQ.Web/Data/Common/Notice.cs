using System;
using System.Collections.Generic;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Data.Common
{
    public class Notice
    {
        public string Id { get; set; }

        /// <summary>
        /// 通知方类型
        /// </summary>
        public NoticeWho Who { get; set; }

        /// <summary>
        /// 消息发送方
        /// Who 为 System 时为 NULL
        ///     为 User   时为 UserId
        /// </summary>
        public string WhoId { get; set; }

        /// <summary>
        /// 通知所属模块
        /// </summary>
        public NoticeWhere Where { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime When { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public NoticeWhat What { get; set; }

        /// <summary>
        /// 通知描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 通知状态
        /// </summary>
        public NoticeState State { get; set; }

        /// <summary>
        /// Who 消息接收者
        /// </summary>
        public virtual ICollection<NoticeReceive> Receivers { get; set; }

        public virtual AppUser Sender { get; set; }
    }
}
