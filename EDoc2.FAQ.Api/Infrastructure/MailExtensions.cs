using EDoc2.FAQ.Notification.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Api.Infrastructure
{
    public static class MailExtensions
    {
        public static void UseMailSender(this IServiceCollection services, IConfiguration configuration)
        {
            var mailConfig = configuration.GetSection("Mail");
            var retryCount = mailConfig.GetValue<int>("RetryCount");
            var host = mailConfig.GetValue<string>("Host");
            var port = mailConfig.GetValue<int>("Port");
            var useSsl = mailConfig.GetValue<bool>("UseSsl");
            var userName = mailConfig.GetValue<string>("UserName");
            var password = mailConfig.GetValue<string>("Password");

            services.AddTransient<IMailSender>(provider =>
            {
                var settings = new MailClientSetting
                {
                    Host = host,
                    Port = port,
                    UseSSL = useSsl,
                    UserName = userName,
                    Password = password
                };
                var logger = provider.GetRequiredService<ILogger<SMTPSender>>();
                return new SMTPSender(settings, logger, retryCount);
            });
        }
    }
}
