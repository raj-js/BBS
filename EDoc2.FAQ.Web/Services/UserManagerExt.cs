using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using EDoc2.FAQ.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Web.Services
{
    public class UserManagerExt : IUserManagerExt
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ArticleManager> _logger;
        private readonly AppDbContext _appDbContext;

        public UserManagerExt(UserManager<AppUser> userManager,
            IServiceProvider provider,
            IMemoryCache memoryCache,
            ILogger<ArticleManager> logger,
            AppDbContext appDbContext)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;

            var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _appDbContext = appDbContext;
            _logger = logger;
        }


        public async Task ModifyClaim(AppUser appUser, string type, string value)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            if (appUser.UserClaims.Any(c => c.ClaimType == type))
            {
                var claim = _appDbContext.UserClaims.Single(c => c.UserId.Equals(appUser.Id) && c.ClaimType == type);
                claim.ClaimValue = value;
            }
            else
            {
                _appDbContext.UserClaims.Add(new AppUserClaim
                {
                    UserId = appUser.Id,
                    ClaimType = type,
                    ClaimValue = value
                });
            }
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ModifyClaims(AppUser appUser, IEnumerable<(string type, string value)> claims)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            var userClaims = _appDbContext.UserClaims.Where(uc => uc.UserId.Equals(appUser.Id));
            foreach (var claim in claims)
            {
                if (userClaims.Any(c => c.ClaimType == claim.type))
                {
                    var src = userClaims.Single(c => c.ClaimType == claim.type);
                    src.ClaimValue = claim.value;
                }
                else
                {
                    _appDbContext.UserClaims.Add(new AppUserClaim
                    {
                        UserId = appUser.Id,
                        ClaimType = claim.type,
                        ClaimValue = claim.value
                    });
                }
            }
            await _appDbContext.SaveChangesAsync();
        }

        public bool IsDailySignIn(AppUser appUser, DateTime when)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            return appUser.DailySignIns.Any(item => item.SignInTime.Date == when.Date);
        }

        public async Task<bool> DailySignIn(AppUser appUser, string ip)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if (string.IsNullOrWhiteSpace(ip))
                throw new ArgumentNullException(nameof(ip));

            //今日已签到
            if (appUser.DailySignIns.Any(item => item.SignInTime.Date == DateTime.Now.Date)) return false;

            //持续签到时间 上面已经排除了今日已经签到的情况
            var keepSignInDays = GetKeepSignInDays(appUser) + 1;
            _appDbContext.DailySignIns.Add(new DailySignIn
            {
                UserId = appUser.Id,
                SignInTime = DateTime.Now,
                ClientIp = ip
            });
            if (await _appDbContext.SaveChangesAsync() == 0) return false;
            await ModifyClaim(appUser, ClaimConsts.KeepSignInDays, keepSignInDays.ToString());

            //根据持续签到天数来判断今日签到获取多少财富值
            var rule = DailySignRule.MatchRule(keepSignInDays);
            if (rule == null)
                throw new IndexOutOfRangeException($"{keepSignInDays}超出了签到规则范围");

            return await AddUserScore(appUser, LogConsts.DailySignIn, $"客户端Ip: {ip}", rule.Score);
        }

        public async Task<bool> AddUserScore(AppUser appUser, string type, string description, int score)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            var userScore = appUser.UserClaims.Get(ClaimConsts.Score, int.Parse);
            userScore += score;
            if (userScore < 0)
                throw new InvalidOperationException("财富值不足");

            await ModifyClaim(appUser, ClaimConsts.Score, userScore.ToString());
            await _appDbContext.LogScores.AddAsync(new LogScore
            {
                UserId = appUser.Id,
                Type = type,
                Description = description,
                Score = score,
                Total = userScore,
                DateTime = DateTime.Now
            });
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public int GetKeepSignInDays(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            //获取上次签到的时间
            var lastSignIn = appUser.DailySignIns
                .OrderByDescending(item => item.SignInTime)
                .FirstOrDefault();
            if (lastSignIn == null) return 0;

            //持续签到天数
            var keepSignInDays = appUser.UserClaims.Get(ClaimConsts.KeepSignInDays, int.Parse);

            //今日已经签到
            if (IsDailySignIn(appUser, DateTime.Now)) return keepSignInDays;

            //昨日没签到
            if (!IsDailySignIn(appUser, DateTime.Now.AddDays(-1))) return 0;

            //今日还没签到
            return keepSignInDays;
        }

        public async Task<List<DailySignIn>> GetDailySignIns<TKey>(Expression<Func<DailySignIn, bool>> where = null, int start = 0, int length = 15, bool isDesc = false, Expression<Func<DailySignIn, TKey>> orderBy = null)
        {
            var query = _appDbContext.DailySignIns.Where(item => true);

            if (where != null)
                query = query.Where(where);

            if (orderBy == null)
                orderBy = e => (TKey)(object)e.Id;

            query = isDesc ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            query = query.Skip(start).Take(length);
            return await query.ToListAsync();
        }

        public async Task<List<AppUserClaim>> GetLongestDailySignIn(int tops)
        {
            if (tops <= 0)
                throw new ArgumentOutOfRangeException(nameof(tops));

            var keepSignInDaysClaims = await _appDbContext.UserClaims
                .Where(item => ClaimConsts.KeepSignInDays.Equals(item.ClaimType))
                .OrderByDescending(item => item.ClaimValue)
                .Take(tops)
                .ToListAsync();
            return keepSignInDaysClaims;
        }

        public string GetUserName(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            var userNameClaim = _appDbContext.UserClaims.SingleOrDefault(item =>
                userId.Equals(item.UserId) && ClaimTypes.Name.Equals(item.ClaimType));

            return userNameClaim?.ClaimValue;
        }

        public async Task<bool> AddFavorite(AppUser appUser, string articleId)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if (string.IsNullOrWhiteSpace(articleId))
                throw new ArgumentNullException(nameof(articleId));

            Expression<Func<ArticleFavorite, bool>> where = item => appUser.Id.Equals(item.UserId) && item.ArticleId.Equals(articleId);

            //变更收藏状态
            if (_appDbContext.ArticleFavorites.Any(where))
            {
                var favorite = _appDbContext.ArticleFavorites.Single(where);
                favorite.State = (int)FavoriteState.Cancel + (int)FavoriteState.Favorite - favorite.State;
            }
            //收藏
            else
            {
                _appDbContext.ArticleFavorites.Add(new ArticleFavorite
                {
                    UserId = appUser.Id,
                    ArticleId = articleId,
                    OperateDate = DateTime.Now,
                    State = FavoriteState.Favorite
                });
            }
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
