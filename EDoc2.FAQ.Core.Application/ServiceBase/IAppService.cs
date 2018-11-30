using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Uow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Core.Application.ServiceBase
{
    public interface IAppService
    {
        IUnitOfWork UnitOfWork { get; set; }

        SignInManager<User> SignInManager { get; set; }

        UserManager<User> UserManager { get; set; }

        IHttpContextAccessor HttpContextAccessor { get; set; }

        ILogger<IAppService> Logger { get; set; }

        User CurrentUser { get; }
    }
}
