using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<User>> GetUsers();

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="user"></param>
        /// <param name="score"></param>
        /// <param name="reasonId">ref: ScoreChangeReason</param>
        void UpdateScore(User user, int score, int reasonId);
    }
}
