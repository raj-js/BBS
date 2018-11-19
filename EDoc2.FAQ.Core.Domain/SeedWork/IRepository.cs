using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
