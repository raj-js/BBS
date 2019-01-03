using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Notifies.Services
{
    public interface INotifyService : IDomainService
    {
        /// <summary>
        /// 新增通知
        /// </summary>
        /// <param name="notify">通知</param>
        /// <returns></returns>
        Task AddNotify(Notify notify);

        /// <summary>
        /// 通过编号获取通知
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Notify> GetNotify(Guid id);

        /// <summary>
        /// 阅读通知
        /// </summary>
        /// <param name="reader">阅读者</param>
        /// <param name="notify">通知</param>
        /// <returns></returns>
        Task ReadNotify(User reader, Notify notify);

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="operator">操作人</param>
        /// <param name="notify">通知</param>
        /// <param name="isSoftDelete">软删除</param>
        /// <returns></returns>
        Task DeleteNotify(User @operator, Notify notify, bool isSoftDelete = true);

        /// <summary>
        /// 获取通知
        /// </summary>
        /// <returns></returns>
        IQueryable<Notify> GetNotifies();

        /// <summary>
        /// 获取用户所有的通知
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="@operator">用户</param>
        /// <returns></returns>
        Task<IQueryable<Notify>> GetNotifies(User @operator);

        /// <summary>
        /// 删除个人通知
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="notify"></param>
        /// <returns></returns>
        Task DeleteMyNotify(User @operator, Notify notify);
    }
}
