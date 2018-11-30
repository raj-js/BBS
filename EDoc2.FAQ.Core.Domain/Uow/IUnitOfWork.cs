using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        Guid UniqueId { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SaveChangesWithDispatchDomainEvents(CancellationToken cancellationToken = default(CancellationToken));
    }
}
