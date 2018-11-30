using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public virtual Role Role { get; set; }
    }
}
