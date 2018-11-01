namespace EDoc2.FAQ.Web.Services
{
    public interface IMailService
    {
        void Send(string email, string subject, string htmlMessage, bool isHtmlBody = true);
    }
}
