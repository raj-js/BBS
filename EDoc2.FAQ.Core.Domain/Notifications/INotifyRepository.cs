using EDoc2.FAQ.Core.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public interface INotifyRepository : IRepository<Notify>
    {
        IQueryable<Notify> GetNotifies();

        IQueryable<Notify> GetNotifiesByUserId(string userId);

        Task<Notify> AddNotify(Notify notify);

        Task<Notify> UpdateNotify(Notify notify);
    }
}
