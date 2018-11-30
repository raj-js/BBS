using EDoc2.FAQ.Core.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Notifications
{
    public interface INotifyRepository : IRepository<Notify>
    {
        IQueryable<Notify> GetNotifies();

        Task<Notify> AddNotify(Notify notify);

        Task<Notify> UpdateNotify(Notify notify);

        Task DeleteNotify(Notify notify);
    }
}
