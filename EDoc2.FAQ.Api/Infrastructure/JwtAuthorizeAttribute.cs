using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace EDoc2.FAQ.Api.Infrastructure
{
    /// <summary>
    /// Jwt Bearer AuthenticationScheme
    /// </summary>
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public JwtAuthorizeAttribute()
        {
            base.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
