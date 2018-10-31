using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EDoc2.FAQ.Web.Data.Identity;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data.Common;

namespace EDoc2.FAQ.Web.Services
{
    public interface IUserManagerExt
    {
        Task ModifyClaim(AppUser appUser, string type, string value);

        Task ModifyClaims(AppUser appUser, IEnumerable<(string type, string value)> claims);

        /// <summary>
        /// 判断指定时间是否签到
        /// </summary>
        /// <param name="appUser"></param>
        /// <param name="when"></param>
        /// <returns></returns>
        bool IsDailySignIn(AppUser appUser, DateTime when);

        Task<bool> DailySignIn(AppUser appUser, string ip);

        Task<bool> AddUserScore(AppUser appUser, string type, string description, int score);

        int GetKeepSignInDays(AppUser appUser);

        Task<List<DailySignIn>> GetDailySignIns<TKey>(Expression<Func<DailySignIn, bool>> where = null, int start = 0,
            int length = 15, bool isDesc = false, Expression<Func<DailySignIn, TKey>> orderBy = null);

        Task<List<AppUserClaim>> GetLongestDailySignIn(int tops);

        string GetUserName(string userId);

        Task<bool> AddFavorite(AppUser appUser, string articleId);
    }
}
