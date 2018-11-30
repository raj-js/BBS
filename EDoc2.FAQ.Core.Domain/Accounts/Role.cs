using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class Role: IdentityRole
    {
        public static Role Administrator = new Role("7F7A85BE-F329-4A48-A353-2B408FCD9AA1", "Administrator");
        public static Role Moderator = new Role("9F19DE2B-229D-4318-AB06-F2A0E847FA09", "Moderator");
        public static Role Member = new Role("797EE019-BAA6-481A-98F0-3E5E4062F71D", "Member");

        public Role(string id, string name)
        {
            Id = id;
            Name = name;
            NormalizedName = name.ToUpperInvariant();
        }


        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
