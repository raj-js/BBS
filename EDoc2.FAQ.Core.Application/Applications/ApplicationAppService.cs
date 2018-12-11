using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Applications.Services;
using EDoc2.FAQ.Core.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Applications.Dtos.ApplicationDtos;

namespace EDoc2.FAQ.Core.Application.Applications
{
    public class ApplicationAppService : AppServiceBase, IApplicationAppService
    {
        private readonly IApplicationService _applicationService;

        public ApplicationAppService(IApplicationService applicationService)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        public async Task<RespWapper> GetSettings()
        {
            var application = await _applicationService.GetApplication(AppConfig.ApplicationId);

            var profile = Profile.From(application);
            return RespWapper<Profile>.Successed(profile);
        }

        public async Task<RespWapper> UpdateSettings(UpdateSettingsReq req)
        {
            var application = req.To();

            await _applicationService.Update(application);
            await _applicationService.UpdateSettings(application, application.Settings.ToArray());
            await UnitOfWork.SaveChangesAsync();

            return RespWapper.Successed();
        }
    }
}
