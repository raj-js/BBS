using System.Text.Encodings.Web;

namespace EDoc2.FAQ.Core.Application.Mails
{
    public static class MailExtensions
    {
        public static void SendConfirmEmail(this IMailService mailService, string email, string link)
        {
            mailService.Send(email, "EDoc2问答社区-账号激活",
                "请通过点击下面的链接来激活您的账号，如果不是本人操作，请无视。<br />" +
                $"<a href='{HtmlEncoder.Default.Encode(link)}'>激活</a>。<br/>" +
                "如果无法跳转，请复制以下链接到浏览器打开。<br />" +
                HtmlEncoder.Default.Encode(link));
        }
    }
}
