using EDoc2.FAQ.Core.Domain.Services;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Applications.Services
{
    public interface IApplicationService : IDomainService
    {
        Task Create(Application application);

        Task Update(Application application);

        Task<Application> GetApplication(Guid applicationId);

        Task UpdateSettings(Application application, params ApplicationSetting[] settings);
    }
}
