using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public interface IAccountRepository : IRepository<User>
    {
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<User>> GetUsers();

        /// <summary>
        /// 根据编号查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> FindAsync(string id);

        /// <summary>
        /// 根据编号查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        User Find(string id);

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="user"></param>
        /// <param name="score"></param>
        /// <param name="reasonId">ref: ScoreChangeReason</param>
        void ChangeScore(User user, int score, int reasonId);

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User Update(User user);

        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="userId"></param>
        void Follow(string userId);

        /// <summary>
        /// 取消关注
        /// </summary>
        void UnFollow(string userId);

        /// <summary>
        /// 加入收藏
        /// </summary>
        /// <param name="articleId"></param>
        void AddFavorite(Guid articleId);

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="articleId"></param>
        void RemoveFavorite(Guid articleId);
    }
}
