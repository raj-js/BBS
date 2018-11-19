using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Integral
{
    public interface IScoreRepository : IRepository<Score>
    {
        /// <summary>
        /// 判断用户是否有足够的积分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        bool HasEnoughScore(string userId, int score);
    }
}
