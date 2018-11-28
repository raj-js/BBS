using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public interface INotifyRepository : IRepository<Notify>
    {
        IQueryable<Notify> GetNotifies();

        IQueryable<Notify> GetNotifiesByUser(User user);

        Task<Notify> AddNotify(Notify notify);

        Task<Notify> UpdateNotify(Notify notify);

        Task DeleteNotify(Notify notify);
    }
}
