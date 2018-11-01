using System.Collections.Generic;

namespace EDoc2.FAQ.Notification.Mail
{
    public class MailEntry
    {
        public (string Name, string Address) From { get; set; }

        public List<(string Name, string Address)> Tos { get; set; }

        public List<(string Name, string Address)> Ccs { get; set; }

        public List<(string Name, string Address)> Bccs { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<(string Name, byte[] Buffer)> Attachments { get; set; }

        public bool IsBodyUseHtml { get; set; }
    }
}
