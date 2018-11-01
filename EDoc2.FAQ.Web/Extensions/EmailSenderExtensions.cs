using EDoc2.FAQ.Web.Services;
using System.Text.Encodings.Web;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class EmailSenderExtensions
    {
        public static void SendEmailConfirmationAsync(this IMailService mailService, string email, string link)
        {
            mailService.Send(email, "EDoc2问答社区-账号激活",
                "请通过点击下面的链接来激活您的账号，如果不是本人操作，请无视。<br />" +
                $"<a href='{HtmlEncoder.Default.Encode(link)}'>激活</a>。<br/>" +
                "如果无法跳转，请复制以下链接到浏览器打开。<br />" +
                HtmlEncoder.Default.Encode(link));
        }

        public static void SendResetPasswordAsync(this IMailService mailService, string email, string callbackUrl)
        {
            mailService.Send(email, "Reset Password",
                $"请通过下面的链接来重置密码： <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>重置密码</a>.");
        }

        public static void SendRegisterCodeAsync(this IMailService mailService, string email, string code, int minutes)
        {
            mailService.Send(email, "EDoc2问答社区-用户注册",
                $"【EDoc2问答社区】验证码是【{code}】, 仅用于注册校验，请勿告诉他人。工作人员不会向您索要。{minutes} 分钟内有效！");
        }
    }
}
