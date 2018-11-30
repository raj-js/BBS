using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EDoc2.FAQ.Core.Application.Aop
{
    public class AuthorizeInterceptorAttribute: AbstractInterceptorAttribute
    {
        private IHttpContextAccessor _contextAccessor;
        private UserManager<User> _userManager;
        private User _currentUser;

        public Role[] AllowRoles { get; set; }

        public AuthorizeInterceptorAttribute(params Role[] allowRoles)
        {
            AllowRoles = allowRoles;
        }

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            _contextAccessor = context.ServiceProvider.GetService<IHttpContextAccessor>();
            _userManager = context.ServiceProvider.GetService<UserManager<User>>();

            var principal = _contextAccessor.HttpContext.User;
            _currentUser = _userManager.GetUserAsync(principal).GetAwaiter().GetResult();

            if (_currentUser.InRoles(AllowRoles))
                throw new UnauthorizedAccessException();

            return next(context);
        }
    }
}
