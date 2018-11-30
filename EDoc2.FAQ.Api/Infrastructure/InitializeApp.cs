using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EDoc2.FAQ.Api.Infrastructure
{
    public static class InitializeApp
    {
        public static void Setup(this IServiceProvider provider)
        {
            using (var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CommunityContext>();

                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(Role.Administrator, Role.Moderator, Role.Member);
                    dbContext.SaveChanges();
                }

                //var accountRepository = scope.ServiceProvider.GetService<IAccountRepository>();
                //var administrator = new User
                //{
                //    UserName = "rajesh.js@live.cn",
                //    Email = "rajesh.js@live.cn",
                //    Nickname = "administrator",
                //    JoinDate = DateTime.Now,
                //    IsMuted = false,
                //    EmailConfirmed = true
                //};
                //var identityResult = accountRepository.CreateAdmin(administrator, "ad123456!")
                //    .GetAwaiter()
                //    .GetResult();

                //var logger = scope.ServiceProvider.GetService<ILogger>();
                //if (!identityResult.Succeeded)
                //{
                //    logger.LogError(identityResult.Errors.ToString());
                //}
            }
        }
    }
}
