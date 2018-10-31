using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using EDoc2.FAQ.Email;
using Microsoft.Extensions.Configuration;

namespace EDoc2.FAQ.Web.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly (string name, string address) _from;
        private readonly (string host, int port, bool useSsl, string userName, string password, Protocol protocol)
            _mailConfig;

        private Postman _postman;
        public Postman Postman => _postman ?? (_postman = new Postman(_mailConfig.host, _mailConfig.port, _mailConfig.useSsl,
                                      _mailConfig.userName, _mailConfig.password, _mailConfig.protocol));

        public EmailSender(IConfiguration configuration)
        {
            var mailConfig = configuration.GetSection("Mail");
            _from = (mailConfig.GetValue<string>("FromName"), mailConfig.GetValue<string>("FromAddress"));
            _mailConfig = (mailConfig.GetValue<string>("Host"),
                mailConfig.GetValue<int>("Port"),
                mailConfig.GetValue<bool>("UseSsl"),
                mailConfig.GetValue<string>("UserName"),
                mailConfig.GetValue<string>("Password"),
                (Protocol)mailConfig.GetValue<int>("Protocol"));
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var to = new List<(string name, string address)>()
            {
                (email, email)
            };
            await Postman.SendAsync(_from, to, null, null, subject, htmlMessage, true);
        }
    }
}
