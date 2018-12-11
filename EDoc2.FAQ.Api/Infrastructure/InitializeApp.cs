using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Applications;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Api.Infrastructure
{
    /// <summary>
    /// 程序数据初始化
    /// </summary>
    public static class InitializeApp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public static void Setup(this IServiceProvider provider)
        {
            using (var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<CommunityContext>();

                //初始化角色
                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(Role.Administrator, Role.Moderator, Role.Member);
                    dbContext.SaveChanges();
                }

                //初始化系统配置
                if (!dbContext.Set<Application>().Any())
                {
                    var application = new Application
                    {
                        Id = AppConfig.ApplicationId,
                        Name = "EDoc2问答社区",
                        Description = "相关描述...",
                        Version = "1.0.0",
                        Settings = new List<ApplicationSetting>
                        {
                            new ApplicationSetting
                            {
                                Name = nameof(ApplicationSetting.IsArticleAuditing),
                                Type = "boolean",
                                Value = true.ToString(),
                                Description = "文章是否需要审核"
                            },

                            new ApplicationSetting
                            {
                                Name = nameof(ApplicationSetting.IsCommentAuditing),
                                Type = "boolean",
                                Value = false.ToString(),
                                Description = "评论是否需要审核"
                            },

                            new ApplicationSetting
                            {
                                Name = nameof(ApplicationSetting.OperationInterval),
                                Type = "int",
                                Value = 3.ToString(),
                                Description = "操作时间间隔（秒）"
                            },

                            new ApplicationSetting
                            {
                                Name = nameof(ApplicationSetting.CacheExpireInterval),
                                Type = "int",
                                Value = 30.ToString(),
                                Description = "缓存过期时间 （分钟）"
                            },

                            new ApplicationSetting
                            {
                                Name = nameof(ApplicationSetting.ViewInterval),
                                Type = "int",
                                Value = 30.ToString(),
                                Description = "单用户/单Ip 查看文章， 增长文章访问量的间隔时长 （分钟）"
                            }
                        }
                    };
                    dbContext.Set<Application>().Add(application);
                    dbContext.SaveChanges();
                }

                //初始化管理员
                var accountService = scope.ServiceProvider.GetService<IAccountService>();
                var administrator = new User
                {
                    UserName = "rajesh.js@live.cn",
                    Email = "rajesh.js@live.cn",
                    Nickname = "administrator",
                    JoinDate = DateTime.Now,
                    EmailConfirmed = true
                };
                accountService.Create(administrator, "ad123456!", true).GetAwaiter().GetResult();
            }
        }
    }
}
