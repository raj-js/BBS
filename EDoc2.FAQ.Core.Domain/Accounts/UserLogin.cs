using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public virtual User User { get; set; }
    }
}
