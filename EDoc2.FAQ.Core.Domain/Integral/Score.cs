using EDoc2.FAQ.Core.Domain.Integral.Events;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Integral
{
    /// <summary>
    /// 积分中心
    /// </summary>
    public class Score : Entity, IAggregateRoot
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// 更新积分
        /// </summary>
        /// <param name="score"></param>
        public void ChangeScore(int score)
        {
            var originScore = TotalScore;
            TotalScore += score;

            AddDomainEvent(new ScoreChangeDomainEvent(UserId, originScore, score, TotalScore));
        }
    }
}
