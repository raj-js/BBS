using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Uow;
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

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ILogger<IAppService> Logger { get; set; }

        private User _currtUser;
        public User CurrentUser => _currtUser ?? (_currtUser = UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User)
                                       .GetAwaiter().GetResult());
    }
}
