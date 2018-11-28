using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    public interface IAccountRepository : IRepository<User>
    {
        /// <summary>
        /// 创建管理员（初始化的时候使用）
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="allowMultipleAdmin"></param>
        Task<IdentityResult> CreateAdmin(User user, string password, bool allowMultipleAdmin = false);

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterAsync(User user, string password);

        /// <summary>
        /// 授权版主
        /// </summary>
        /// <param name="operator">操作人</param>
        /// <param name="userId">关联用户编号</param>
        /// <param name="moduleId">模块编号</param>
        /// <returns></returns>
        Task<IdentityResult> GrantModerator(User @operator, string userId, Guid moduleId);

        /// <summary>
        /// 撤销版主
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="userId">关联用户编号</param>
        /// <returns></returns>
        Task<IdentityResult> RecycleModerator(User @operator, string userId);

        /// <summary>
        /// 更替版主
        /// </summary>
        /// <param name="operator">操作人</param>
        /// <param name="grantUserId">授权版主编号</param>
        /// <param name="recycleUserId">撤销版主编号</param>
        /// <param name="moduleId">板块编号</param>
        /// <returns></returns>
        Task ChangeModerator(User @operator, string grantUserId, string recycleUserId, string moduleId);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        IQueryable<User> GetUsers(bool skipAdmin = true);

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
        /// 关注用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        Task Follow(User user, string userId);

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        Task UnFollow(User user, string userId);

        /// <summary>
        /// 获取用户的所有关注
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IQueryable<User> GetFollows(User user);

        /// <summary>
        /// 获取用户所有粉丝
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        IQueryable<User> GetFans(User user);

        /// <summary>
        /// 加入收藏
        /// </summary>
        /// <param name="user"></param>
        /// <param name="articleId"></param>
        Task AddFavorite(User user, Guid articleId);

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="user"></param>
        /// <param name="articleId"></param>
        /// <returns></returns>
        Task RemoveFavorite(User user, Guid articleId);

        /// <summary>
        /// 获取用户收藏文章
        /// </summary>
        /// <returns></returns>
        IQueryable<UserFavorite> GetFavorites(User user);
    }
}
