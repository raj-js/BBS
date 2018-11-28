using EDoc2.FAQ.Core.Domain.SeedWork;
using System;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public class Notify : Entity<Guid>, IAggregateRoot
    {
        /// <summary>
        /// 发起对象类型
        /// </summary>
        public virtual NotifyInitiatorType InitiatorType { get; set; }

        /// <summary>
        /// 发起对象
        /// </summary>
        public string InitiatorId { get; set; }

        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime InitiationTime { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public virtual NotifyOperationType OperationType { get; set; }

        /// <summary>
        /// 关联对象类型
        /// </summary>
        public virtual NotifyRelationObjectType RelationObjectType { get; set; }

        /// <summary>
        /// 关联对象编号
        /// </summary>
        public string RelationObjectId { get; set; }

        /// <summary>
        /// 被通知对象的类型
        /// </summary>
        public virtual NotifyToObjectType ToObjectType { get; set; }

        /// <summary>
        /// 被通知对象编号
        /// </summary>
        public string ToObjectId { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEffective { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// 设置过期
        /// </summary>
        public void SetExpire()
        {
            IsEffective = false;
            ExpirationTime = DateTime.Now;
        }
    }
}
