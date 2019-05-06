using EDoc2.FAQ.Api.Infrastructure;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .Build()
                .MigrateDbContext<CommunityContext>((context, services) =>
                {
                    var accountService = services.GetService<IAccountService>();
                    var logger = services.GetService<ILogger<DbContextSeed>>();

                    new DbContextSeed()
                    .SeedAsync(context, accountService, logger)
                    .Wait();
                });
            //host.Services.Setup();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseStartup<Startup>();
    }
}
