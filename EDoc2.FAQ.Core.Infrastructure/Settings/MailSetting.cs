namespace EDoc2.FAQ.Core.Infrastructure.Settings
{
    public class MailSetting
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public int RetryCount { get; set; }
    }
}
