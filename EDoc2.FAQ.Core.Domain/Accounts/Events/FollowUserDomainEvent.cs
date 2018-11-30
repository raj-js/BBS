using System;
using MediatR;

namespace EDoc2.FAQ.Core.Domain.Accounts.Events
{
    public class FollowUserDomainEvent : INotification
    {
        /// <summary>
        /// 关注者
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// 被关注者的编号
        /// </summary>
        public string FollowId { get; private set; }

        /// <summary>
        /// 操作时间/关注时间
        /// </summary>
        public DateTime OperationTime { get; private set; }

        public FollowUserDomainEvent(User user, string followId, DateTime operationTime)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            FollowId = followId ?? throw new ArgumentNullException(nameof(followId));
            OperationTime = operationTime;
        }
    }
}
