using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Notifies
{
    public interface INotifyRepository : IRepository<Notify>
    {
        IQueryable<Notify> GetNotifies();

        Task<Notify> FindNotifyById(Guid id);

        Task AddNotify(Notify notify);

        Task UpdateNotify(Notify notify, params string[] properties);

        Task DeleteNotify(Notify notify);

        IQueryable<NotifyBelong> GetNotifyBelongTo();
    }
}
