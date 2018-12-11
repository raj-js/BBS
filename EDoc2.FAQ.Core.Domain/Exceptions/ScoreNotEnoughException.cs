namespace EDoc2.FAQ.Core.Domain.Exceptions
{
    public class ScoreNotEnoughException : DomainException
    {
        public string UserId { get; }

        public int Score { get; }

        public int Spent { get; }

        public ScoreNotEnoughException(string userId, int score, int spent)
            : base($"编号为'{userId}'的用户拥有积分'{score}'，尝试消耗'{spent}'积分失败")
        {
            UserId = userId;
            Score = score;
            Spent = spent;
        }
    }
}
