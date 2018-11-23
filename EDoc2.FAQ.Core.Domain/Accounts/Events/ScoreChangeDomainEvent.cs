using MediatR;

namespace EDoc2.FAQ.Core.Domain.Applications.Events
{
    public class ScoreChangeDomainEvent : INotification
    {
        public string UserId { get; set; }

        public int OriginScore { get; set; }

        public int ChangeScore { get; set; }

        public int FinalScore { get; set; }

        public ScoreChangeDomainEvent(string userId, int originScore, int changeScore, int finalScore)
        {
            UserId = userId;
            OriginScore = originScore;
            ChangeScore = changeScore;
            FinalScore = finalScore;
        }
    }
}
