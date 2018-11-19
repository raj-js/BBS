using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
