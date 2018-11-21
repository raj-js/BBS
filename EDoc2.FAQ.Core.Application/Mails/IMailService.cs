namespace EDoc2.FAQ.Core.Application.Mails
{
    public interface IMailService
    {
        void Send(string email, string subject, string htmlMessage, bool isHtmlBody = true);
    }
}
