using System.Collections.Generic;
using EDoc2.FAQ.Core.Application.IntegrationEvents;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.Notification.Mail;
using Microsoft.Extensions.Configuration;

namespace EDoc2.FAQ.Core.Application.Mails
{
    public class MailService : IMailService
    {
        private readonly IEventBus _eventBus;
        private readonly IConfigurationSection _mailSection;

        public MailService(
            IEventBus eventBus,
            IConfiguration configuration
        )
        {
            _eventBus = eventBus;
            _mailSection = configuration.GetSection("Mail");
        }

        public void Send(string email, string subject, string htmlMessage, bool isHtmlBody = true)
        {
            var fromName = _mailSection.GetValue<string>("FromName");
            var fromAddress = _mailSection.GetValue<string>("FromAddress");

            var mailSendEvent = new MailSendEvent
            {
                Entry = new MailEntry
                {
                    From = (Name: fromName, Address: fromAddress),
                    Tos = new List<(string Name, string Address)>
                    {
                        (Name:email, Address:email)
                    },
                    IsBodyUseHtml = true,
                    Subject = subject,
                    Body = htmlMessage
                }
            };
            _eventBus.Publish(mailSendEvent);
        }
    }
}
