using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Email
{
    public enum Protocol
    {
        Pop3 = 0,
        Smtp = 1
    }

    /// <summary>
    /// 暂实现 Stmp
    /// </summary>
    public class Postman
    {
        private readonly Protocol _protocol;
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useSsl;
        private readonly string _userName;
        private readonly string _password;

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public Postman(string host, int port, bool useSsl, string userName, string password, Protocol protocol = Protocol.Smtp)
        {
            _protocol = protocol;
            _host = host;
            _port = port;
            _useSsl = useSsl;
            _userName = userName;
            _password = password;
        }

        private MimeMessage CreateMsg((string name, string address) from, IEnumerable<(string name, string address)> to,
            IReadOnlyCollection<(string name, string address)> ccs, IReadOnlyCollection<(string name, string address)> bccs, 
            string subject, string body, bool isHtml = false, List<(string name, byte[] bytes)> attachments = null)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(Encoding, @from.name, @from.address));
            msg.To.AddRange(to.Select(item => new MailboxAddress(Encoding, item.name, item.address)));

            if (ccs != null)
                msg.Cc.AddRange(ccs.Select(item => new MailboxAddress(Encoding, item.name, item.address)));

            if (bccs != null)
                msg.Bcc.AddRange(bccs.Select(item => new MailboxAddress(Encoding, item.name, item.address)));

            msg.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            if (isHtml)
                bodyBuilder.HtmlBody = body;
            else
                bodyBuilder.TextBody = body;

            attachments?.ForEach(item => { bodyBuilder.Attachments.Add(item.name, item.bytes); });
            msg.Body = bodyBuilder.ToMessageBody();
            return msg;
        }

        private async Task SendSmtp(MimeMessage msg)
        {
            using (var smtp = new SmtpClient())
            {
                smtp.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                await smtp.ConnectAsync(_host, _port, _useSsl);
                smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                await smtp.AuthenticateAsync(_userName, _password);
                await smtp.SendAsync(msg);
                await smtp.DisconnectAsync(true);
            }
        }

        public async Task SendAsync((string name, string address) from, IEnumerable<(string name, string address)> to,
            IReadOnlyCollection<(string name, string address)> ccs, IReadOnlyCollection<(string name, string address)> bccs,
            string subject, string body, bool isHtml = false, List<(string name, byte[] bytes)> attachments = null)
        {
            var msg = CreateMsg(from, to, ccs, bccs, subject, body, isHtml, attachments);
            switch (_protocol)
            {
                case Protocol.Pop3:
                {
                    break;
                }
                case Protocol.Smtp:
                {
                    await SendSmtp(msg);
                    break;
                }
            }
        }
    }
}
