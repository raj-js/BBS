using EDoc2.FAQ.Core.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Applications.Services
{
    public class ApplicationService : DomainService, IApplicationService
    {
        private readonly IApplicationRepository _applicationRepo;

        public ApplicationService(IApplicationRepository applicationRepo)
        {
            _applicationRepo = applicationRepo ?? throw new ArgumentNullException(nameof(applicationRepo));
        }

        public async Task Create(Application application)
        {
            await _applicationRepo.Create(application);
        }

        public async Task Update(Application application)
        {
            await _applicationRepo.Update(application,
                nameof(Application.Name),
                nameof(Application.IconBase64),
                nameof(Application.Version),
                nameof(Application.Description));
        }

        public Application GetApplication(Guid applicationId)
        {
            return _applicationRepo.FindById(applicationId);
        }

        public async Task UpdateSettings(Application application, params ApplicationSetting[] settings)
        {
            settings.ToList()
                .ForEach(s => _applicationRepo.UpdateSetting(s, nameof(ApplicationSetting.Value)));

            await Task.CompletedTask;
        }
    }
}
