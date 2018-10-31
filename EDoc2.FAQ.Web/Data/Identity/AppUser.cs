using System.Collections.Generic;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EDoc2.FAQ.Web.Data.Identity
{
    public class AppUser : IdentityUser
    {
        public string CustomTag { get; set; }
        public virtual ICollection<AppUserClaim> UserClaims { get; set; }
        public virtual ICollection<AppUserLogin> UserLogins { get; set; }
        public virtual ICollection<AppUserToken> UserTokens { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
        public virtual ICollection<ArticleFavorite> FavoriteArticles { get; set; }
        public virtual ICollection<NoticeReceive> NoticeReceives { get; set; }
        public virtual ICollection<CommentOp> CommentOps { get; set; }
        public virtual ICollection<LogScore> LogScores { get; set; }
        public virtual ICollection<DailySignIn> DailySignIns { get; set; }
    }
}
