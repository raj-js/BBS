using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<AppUserClaim> UserClaims { get; set; }
        public virtual ICollection<AppUserLogin> UserLogins { get; set; }
        public virtual ICollection<AppUserToken> UserTokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
