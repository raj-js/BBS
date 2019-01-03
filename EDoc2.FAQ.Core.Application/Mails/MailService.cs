using EDoc2.FAQ.Core.Application.IntegrationEvents;
using EDoc2.FAQ.Core.Infrastructure.Settings;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.Notification.Mail;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Application.Mails
{
    public class MailService : IMailService
    {
        private readonly IEventBus _eventBus;
        private readonly MailSetting _mailSetting;

        public MailService(
            IEventBus eventBus,
            IOptions<MailSetting> mailSetting
        )
        {
            _eventBus = eventBus;
            _mailSetting = mailSetting.Value;
        }

        public void Send(string email, string subject, string htmlMessage, bool isHtmlBody = true)
        {
            var mailSendEvent = new MailSendEvent
            {
                Entry = new MailEntry
                {
                    From = (Name: _mailSetting.FromName, Address: _mailSetting.FromAddress),
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
