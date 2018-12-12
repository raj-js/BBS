using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Applications.Services;
using EDoc2.FAQ.Core.Domain.Uow;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Core.Application.ServiceBase
{
    public class AppServiceBase
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        public UserManager<User> UserManager { get; set; }

        public IAccountService AccountService { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ILogger<IAppService> Logger { get; set; }

        public IApplicationService ApplicationService { get; set; }

        private User _currtUser;
        public User CurrentUser => _currtUser ?? (_currtUser = AccountService.FindUserById(UserManager.GetUserId(HttpContextAccessor.HttpContext.User)));

        private Domain.Applications.Application _application;
        public Domain.Applications.Application Application => _application ?? (_application = ApplicationService.GetApplication(AppConfig.ApplicationId));
    }
}
