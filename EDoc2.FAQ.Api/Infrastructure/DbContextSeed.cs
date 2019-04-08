using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Api.Infrastructure
{
    public class DbContextSeed
    {
        public async Task SeedAsync(CommunityContext context, IAccountService accountService, ILogger<DbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CommunityContext));

            await policy.ExecuteAsync(async () =>
            {
                context.Database.Migrate();

                //初始化角色
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(Role.Administrator, Role.Moderator, Role.Member);
                    await context.SaveChangesAsync();
                }

                //初始化系统配置
                if (!context.Set<Application>().Any())
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
                    context.Set<Application>().Add(application);
                    await context.SaveChangesAsync();
                }

                //初始化管理员
                var administrator = new User
                {
                    UserName = "rajesh.js@live.cn",
                    Email = "rajesh.js@live.cn",
                    Nickname = "administrator",
                    JoinDate = DateTime.Now,
                    EmailConfirmed = true
                };
                await accountService.Create(administrator, "ad123456!", true);
            });
        }

        private Policy CreatePolicy(ILogger<DbContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
