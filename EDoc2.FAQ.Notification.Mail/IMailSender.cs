using System.Threading.Tasks;

namespace EDoc2.FAQ.Notification.Mail
{
    public interface IMailSender
    {
        bool IsConnect { get; }

        bool TryConnect();

        Task SendAsync(MailEntry entry);
    }
}
