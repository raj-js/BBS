using System;
using MediatR;

namespace EDoc2.FAQ.Core.Domain.Accounts.Events
{
    public class UnFollowUserDomainEvent : INotification
    {
        /// <summary>
        /// 关注者
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// 被关注者的编号
        /// </summary>
        public string FollowId { get; private set; }

        public UnFollowUserDomainEvent(User user, string followId)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            FollowId = followId ?? throw new ArgumentNullException(nameof(followId));
        }
    }
}
