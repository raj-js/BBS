using EDoc2.FAQ.Core.Domain.SeedWork;
using EDoc2.FAQ.Core.Domain.Uow;

namespace EDoc2.FAQ.Core.Domain.Repositories
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
