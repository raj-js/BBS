using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public class AppUserToken : IdentityUserToken<string>
    {
        public virtual AppUser User { get; set; }
    }
}
