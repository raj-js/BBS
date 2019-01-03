using EDoc2.FAQ.Core.Infrastructure.Settings;
using EDoc2.FAQ.Notification.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Api.Infrastructure
{
    public static class MailExtensions
    {
        public static void UseMailSender(this IServiceCollection services, MailSetting setting)
        {
            services.AddTransient<IMailSender>(provider =>
            {
                var settings = new MailClientSetting
                {
                    Host = setting.Host,
                    Port = setting.Port,
                    UseSSL = setting.UseSsl,
                    UserName = setting.UserName,
                    Password = setting.Password
                };
                var logger = provider.GetRequiredService<ILogger<SMTPSender>>();
                return new SMTPSender(settings, logger, setting.RetryCount);
            });
        }
    }
}
