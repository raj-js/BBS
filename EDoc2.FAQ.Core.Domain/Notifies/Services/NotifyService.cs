using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Notifies.Analyzer;
using EDoc2.FAQ.Core.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Notifies.Services
{
    public class NotifyService : DomainService, INotifyService
    {
        private readonly INotifyRepository _notifyRepo;

        public NotifyService(INotifyRepository notifyRepo)
        {
            _notifyRepo = notifyRepo ?? throw new ArgumentNullException(nameof(notifyRepo));
        }

        public async Task AddNotify(Notify notify)
        {
            if (notify == null)
                throw new ArgumentNullException(nameof(notify));

            await _notifyRepo.AddNotify(notify);
        }

        public async Task DeleteNotify(User @operator, Notify notify, bool isSoftDelete = true)
        {
            if (@operator == null)
                throw new ArgumentNullException(nameof(@operator));

            if (!@operator.IsAdministrator)
                throw new UnauthorizedAccessException();

            if (notify == null)
                throw new ArgumentNullException(nameof(notify));

            if (isSoftDelete)
                notify.SetDeleted();
            else
                await _notifyRepo.DeleteNotify(notify);
        }

        public IQueryable<Notify> GetNotifies()
        {
            return _notifyRepo.GetNotifies();
        }

        public async Task<IQueryable<Notify>> GetNotifies(User @operator)
        {
            var toObjects = NotifyAnalyzer.FilterToObjects(@operator);
            var query = _notifyRepo.GetNotifies();
            query = NotifyAnalyzer.FiterNotifies(query, toObjects);

            // 消息必须有效
            query = query.Where(s => !s.IsDeleted &&
                                     (s.ExpirationTime.HasValue && DateTime.Now < s.ExpirationTime));

            // 排除已删除的消息
            var notifyIds = await _notifyRepo.GetNotifyBelongTo()
                .Where(s => s.UserId == @operator.Id && s.IsDeleted)
                .Select(s => s.NotifyId)
                .ToListAsync();

            query = query.Where(s => !notifyIds.Contains(s.Id));
            return query;
        }

        public async Task DeleteMyNotify(User @operator, Notify notify)
        {
            if (@operator == null)
                throw new ArgumentNullException(nameof(@operator));

            if (notify == null)
                throw new ArgumentNullException(nameof(notify));

            if (!await IsBelongTo(@operator, notify)) return;

            notify.DeleteBy(@operator.Id);
        }

        public async Task<Notify> GetNotify(Guid id)
        {
            return await _notifyRepo.FindNotifyById(id);
        }

        public async Task ReadNotify(User @operator, Notify notify)
        {
            if (!await IsBelongTo(@operator, notify))
                throw new InvalidOperationException();

            notify.ReadBy(@operator.Id);
        }

        /// <summary>
        /// 查询消息是否指向此用户
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="notify"></param>
        /// <returns></returns>
        private async Task<bool> IsBelongTo(User @operator, Notify notify)
        {
            var notifyIds = await _notifyRepo.GetNotifyBelongTo()
                .Where(s => s.UserId == @operator.Id && s.IsDeleted)
                .Select(s => s.NotifyId)
                .ToListAsync();

            // 消息已删除
            if (notifyIds.Contains(notify.Id)) return false;

            var toObjects = NotifyAnalyzer.FilterToObjects(@operator);
            var query = _notifyRepo.GetNotifies();
            query = NotifyAnalyzer.FiterNotifies(query, toObjects);

            // 消息必须有效
            query = query.Where(s => !s.IsDeleted &&
                                     (s.ExpirationTime.HasValue && DateTime.Now < s.ExpirationTime));

            return await query.AnyAsync(s => s.Id == notify.Id);
        }
    }
}
