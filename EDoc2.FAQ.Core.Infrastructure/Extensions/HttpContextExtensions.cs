using Microsoft.AspNetCore.Http;
using System.Linq;

namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetClientIp(this HttpContext context)
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
