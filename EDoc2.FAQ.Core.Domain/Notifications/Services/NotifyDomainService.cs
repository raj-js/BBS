using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Notifications.Services
{
    public class NotifyDomainService : INotifyDomainService
    {
        private readonly INotifyRepository _notifyRepo;
        private readonly IUnitOfWork UnitOfWork;

        public NotifyDomainService(INotifyRepository notifyRepo)
        {
            _notifyRepo = notifyRepo ?? throw new ArgumentNullException(nameof(notifyRepo));
            UnitOfWork = _notifyRepo.UnitOfWork;
        }

        public async Task<Notify> AddNotify(Notify notify)
        {
            if (notify == null)
                throw new ArgumentNullException(nameof(notify));

            await _notifyRepo.AddNotify(notify);
            await UnitOfWork.SaveEntitiesAsync();

            return notify;
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

            await UnitOfWork.SaveEntitiesAsync();
        }

        public IQueryable<Notify> GetNotifiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Notify>> GetNotifiesAsync(User reader)
        {
            throw new NotImplementedException();
        }

        public Task<Notify> GetNotify(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task ReadNotify(User reader, Notify notify)
        {
            throw new NotImplementedException();
        }
    }
}
