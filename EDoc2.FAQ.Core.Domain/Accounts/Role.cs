using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class Role: IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
