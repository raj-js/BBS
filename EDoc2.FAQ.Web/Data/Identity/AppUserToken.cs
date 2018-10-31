using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Web.Data.Identity
{
    public class AppUserToken : IdentityUserToken<string>
    {
        public virtual AppUser User { get; set; }
    }
}
