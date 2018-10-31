using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Web.Data.Identity
{
    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }
    }
}
