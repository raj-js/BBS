using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Applications.Dtos.ApplicationDtos;

namespace EDoc2.FAQ.Core.Application.Applications
{
    public interface IApplicationAppService : IAppService
    {
        Task<RespWapper> GetSettings();

        Task<RespWapper> UpdateSettings(UpdateSettingsReq req);
    }
}
