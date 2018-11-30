using EDoc2.FAQ.Core.Domain.Uow;

namespace EDoc2.FAQ.Core.Domain.Services
{
    public interface IDomainService
    {
        IUnitOfWork UnitOfWork { get; set; }
    }
}
