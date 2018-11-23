using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
