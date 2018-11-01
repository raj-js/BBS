using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Polly;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Notification.Mail
{
    public class SMTPSender : IMailSender, IDisposable
    {
        Encoding UTF8 = Encoding.UTF8;

        private readonly MailClientSetting _settings;
        private readonly SmtpClient _smtpClient;
        private readonly ILogger<SMTPSender> _logger;

        private int _retryCount;
        private bool _disposed;

        public SMTPSender(
            MailClientSetting settings,
            ILogger<SMTPSender> logger,
            int retryCount = 3
            )
        {
            _settings = settings;
            _logger = logger;
            _retryCount = retryCount;

            _smtpClient = new SmtpClient();
        }

        public bool IsConnect => _smtpClient != null && _smtpClient.IsConnected && !_disposed;

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            try
            {
                _smtpClient.Disconnect(true);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.ToString());
            }
        }

        public bool TryConnect()
        {
            if (IsConnect) return false;

            var policy = Policy.Handle<SocketException>()
                .WaitAndRetry(_retryCount, provider => TimeSpan.FromSeconds(1 << _retryCount), (e, span) => 
                {
                    _logger.LogWarning(e.ToString());
                });

            policy.Execute(()=> 
            {
                _smtpClient.Connect(_settings.Host, _settings.Port, _settings.UseSSL);
                _smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                _smtpClient.Authenticate(_settings.UserName, _settings.Password);
            });

            if (IsConnect)
            {
                _smtpClient.MessageSent += _smtpClient_MessageSent;
                _logger.LogInformation("SMTP Client has been connected");
                return true;
            }

            _logger.LogError("SMTP Client connect failed");
            return false;
        }

        private void _smtpClient_MessageSent(object sender, MessageSentEventArgs e)
        {
            _logger.LogDebug(e.Message.ToString());
        }

        public async Task SendAsync(MailEntry entry)
        {
            if (!IsConnect)
                TryConnect();

            if (!IsConnect)
            {
                _logger.LogError("SMTP Client connect failed, please check the mail settings");
                return;
            }

            await _smtpClient.SendAsync(CreateMessage(entry));
        }

        #region privates

        private MimeMessage CreateMessage(MailEntry entry)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress(UTF8, entry.From.Name, entry.From.Address));

            entry.Tos.ForEach(((string Name, string Address) To) =>
            {
                msg.To.Add(new MailboxAddress(UTF8, To.Name, To.Address));
            });

            entry.Ccs?.ForEach(((string Name, string Address) Cc) =>
            {
                msg.Cc.Add(new MailboxAddress(UTF8, Cc.Name, Cc.Address));
            });

            entry.Bccs?.ForEach(((string Name, string Address) Bcc) =>
            {
                msg.Bcc.Add(new MailboxAddress(UTF8, Bcc.Name, Bcc.Address));
            });

            msg.Subject = entry.Subject;

            var bodyBuilder = new BodyBuilder();
            if (entry.IsBodyUseHtml)
                bodyBuilder.HtmlBody = entry.Body;
            else
                bodyBuilder.TextBody = entry.Body;

            entry.Attachments?.ForEach(((string Name, byte[] Buffer) Attachment) => 
            {
                bodyBuilder.Attachments.Add(Attachment.Name, Attachment.Buffer);
            });

            msg.Body = bodyBuilder.ToMessageBody();
            return msg;
        }

        #endregion
    }
}
