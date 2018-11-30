using EDoc2.FAQ.Core.Domain.Uow;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Core.Domain.Services
{
    public class DomainService
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public virtual ILogger<DomainService> Logger { get; set; }
    }
}
