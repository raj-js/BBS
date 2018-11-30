using EDoc2.FAQ.Core.Domain.Uow;

namespace EDoc2.FAQ.Core.Domain.Repositories
{
    public class RepositoryBase
    {
        public IUnitOfWork UnitOfWork { get; set; }
    }
}
