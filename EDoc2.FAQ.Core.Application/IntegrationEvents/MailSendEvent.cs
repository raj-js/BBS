using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.Events;
using EDoc2.FAQ.Notification.Mail;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Application.IntegrationEvents
{
    public class MailSendEvent : IntegrationEvent
    {
        public MailEntry Entry { get; set; }
    }

    public class MailSendEventHandler : IIntegrationEventHandler<MailSendEvent>
    {
        private readonly IMailSender _sender;

        public MailSendEventHandler(IMailSender sender)
        {
            _sender = sender;
        }

        public async Task HandleAsync(MailSendEvent @event)
        {
            await _sender.SendAsync(@event.Entry);
        }
    }
}
