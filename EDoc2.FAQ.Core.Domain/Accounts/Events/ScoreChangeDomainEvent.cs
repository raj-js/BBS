using System;
using MediatR;

namespace EDoc2.FAQ.Core.Domain.Accounts.Events
{
    /// <summary>
    /// 积分变动事件
    /// </summary>
    public class ScoreChangeDomainEvent : INotification
    {
        public User User { get; private set; }

        public int OriginScore { get; private set; }

        public int ChangeScore { get; private set; }

        public UserScoreChangeReason Reason { get; private set; }

        public ScoreChangeDomainEvent(User user, int originScore, int changeScore, UserScoreChangeReason reason)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            OriginScore = originScore;
            ChangeScore = changeScore;
            Reason = reason;
        }
    }
}
