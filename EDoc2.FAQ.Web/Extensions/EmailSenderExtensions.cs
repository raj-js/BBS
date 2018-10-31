using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "EDoc2问答社区-账号激活",
                "请通过点击下面的链接来激活您的账号，如果不是本人操作，请无视。<br />" +
                $"<a href='{HtmlEncoder.Default.Encode(link)}'>激活</a>。<br/>" +
                "如果无法跳转，请复制以下链接到浏览器打开。<br />" +
                HtmlEncoder.Default.Encode(link));
        }

        public static Task SendResetPasswordAsync(this IEmailSender emailSender, string email, string callbackUrl)
        {
            return emailSender.SendEmailAsync(email, "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        public static Task SendRegisterCodeAsync(this IEmailSender emailSender, string email, string code, int minutes)
        {
            return emailSender.SendEmailAsync(email, "EDoc2问答社区-用户注册",
                $"【EDoc2问答社区】验证码是【{code}】, 仅用于注册校验，请勿告诉他人。工作人员不会向您索要。{minutes} 分钟内有效！");
        }
    }
}
