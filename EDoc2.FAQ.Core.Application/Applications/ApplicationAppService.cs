using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System.Linq;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Applications.Dtos.ApplicationDtos;

namespace EDoc2.FAQ.Core.Application.Applications
{
    public class ApplicationAppService : AppServiceBase, IApplicationAppService
    {
        public async Task<RespWapper> GetSettings()
        {
            var profile = Profile.From(Application);
            await Task.CompletedTask;
            return RespWapper<Profile>.Successed(profile);
        }

        public async Task<RespWapper> UpdateSettings(UpdateSettingsReq req)
        {
            var application = req.To();

            await ApplicationService.Update(application);
            await ApplicationService.UpdateSettings(application, application.Settings.ToArray());
            await UnitOfWork.SaveChangesAsync();

            return RespWapper.Successed();
        }
    }
}
