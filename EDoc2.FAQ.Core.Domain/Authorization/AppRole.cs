using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public class AppRole: IdentityRole
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }
}
