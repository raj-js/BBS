using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Accounts;

namespace EDoc2.FAQ.Core.Domain.Notifications.Services
{
    public interface INotifyDomainService
    {
        /// <summary>
        /// 新增通知
        /// </summary>
        /// <param name="notify">通知</param>
        /// <returns></returns>
        Task<Notify> AddNotify(Notify notify);

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
        IQueryable<Notify> GetNotifiesAsync();

        /// <summary>
        /// 获取用户所有的通知
        /// </summary>
        /// <param name="reader">用户</param>
        /// <returns></returns>
        Task<IQueryable<Notify>> GetNotifiesAsync(User reader);
    }
}
