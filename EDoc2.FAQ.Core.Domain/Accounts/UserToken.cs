using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    public class UserToken : IdentityUserToken<string>
    {
        public virtual User User { get; set; }
    }
}
