using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using EDoc2.FAQ.Core.Domain.Accounts;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public class NotifyBox : Entity<Guid>
    {
        /// <summary>
        /// 通知编号
        /// </summary>
        public Guid NotifyId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsReaded { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        public virtual Notify Notify { get; set; }

        public virtual User User { get; set; }

        public NotifyBox()
        {

        }

        /// <summary>
        /// 创建即阅读
        /// </summary>
        /// <param name="notifyId"></param>
        /// <param name="userId"></param>
        public NotifyBox(Guid notifyId, string userId)
        {
            Id = Guid.NewGuid();
            NotifyId = notifyId;
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));

            SetReaded();
        }

        /// <summary>
        /// 设置为已读
        /// </summary>
        public void SetReaded()
        {
            IsReaded = true;
            ReadTime = DateTime.Now;
        }

        /// <summary>
        /// 设置为删除
        /// </summary>
        public void SetDeleted()
        {
            IsDeleted = true;
            DeleteTime = DateTime.Now;
        }
    }
}
