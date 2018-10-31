using Microsoft.AspNetCore.Http;
using System.Linq;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class Utility
    {
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
