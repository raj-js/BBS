using EDoc2.FAQ.Web.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EDoc2.FAQ.Web.Extensions
{
    public static class ClaimConsts
    {
        public const string JoinDate = "JoinDate";
        public const string ComeFrom = "ComeFrom";
        public const string Level = "Level";
        public const string Signature = "Signature";
        public const string Score = "Score";
        public const string KeepSignInDays = "KeepSignInDays";

        public static Claim ForType(string type) => new Claim(type, string.Empty);
    }

    public static class ClaimExtensions
    {
        public static T Get<T>(this ICollection<AppUserClaim> @this, string claimType, Func<string, T> converter = null)
        {
            var claim = @this.Get(claimType);
            if (claim == null) return default(T);

            if (converter != null)
                return converter(claim.ClaimValue);

            object obj = claim.ClaimValue;
            return (T) obj;
        }

        public static AppUserClaim Get(this ICollection<AppUserClaim> @this, string claimType)
        {
            if (string.IsNullOrWhiteSpace(claimType))
                throw new ArgumentNullException(nameof(claimType));

            var claim = @this.SingleOrDefault(item => claimType.Equals(item.ClaimType));
            return claim;
        }
    }
}
