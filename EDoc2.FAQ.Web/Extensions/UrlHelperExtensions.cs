using EDoc2.FAQ.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl = "Home/Index")
        {
            if (!urlHelper.IsLocalUrl(localUrl))
            {
                return urlHelper.Action(
                    action: nameof(HomeController.Index),
                    controller: "Home");
            }
            return localUrl;
        }

        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string returnUrl, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code, returnUrl },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
