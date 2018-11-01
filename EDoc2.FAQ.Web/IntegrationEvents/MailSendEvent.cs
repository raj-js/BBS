using System.Threading.Tasks;
using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.Events;
using EDoc2.FAQ.Notification.Mail;

namespace EDoc2.FAQ.Web.IntegrationEvents
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
