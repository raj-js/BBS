using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Notifies
{
    /// <summary>
    /// 消息箱
    /// </summary>
    public class NotifyBelong : Entity<Guid>
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

        public NotifyBelong() { }

        /// <summary>
        /// 创建即阅读
        /// </summary>
        /// <param name="notifyId"></param>
        /// <param name="userId"></param>
        /// <param name="setReaded"></param>
        public NotifyBelong(Guid notifyId, string userId, bool setReaded = true)
        {
            Id = Guid.NewGuid();
            NotifyId = notifyId;
            UserId = userId;

            if (setReaded)
            {
                IsReaded = true;
                ReadTime = DateTime.Now;
            }
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
