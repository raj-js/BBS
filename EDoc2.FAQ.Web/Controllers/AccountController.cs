using EDoc2.FAQ.ImageCode;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using EDoc2.FAQ.Web.Extensions;
using EDoc2.FAQ.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SysFile = System.IO.File;

namespace EDoc2.FAQ.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserManagerExt _userManagerExt;
        private readonly IArticleManager _articleManager;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSender _emailSender;

        public AccountController(SignInManager<AppUser> signInManager,
            IUserManagerExt userManagerExt,
            IArticleManager articleManager,
            UserManager<AppUser> userManager,
            IMemoryCache memoryCache,
            ILogger<AccountController> logger,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _articleManager = articleManager;
            _userManager = userManager;
            _userManagerExt = userManagerExt;
            _memoryCache = memoryCache;
            _logger = logger;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Home(string accountId = null)
        {
            var vm = new VmAccount();

            AppUser appUser;
            if (!string.IsNullOrWhiteSpace(accountId))
            {
                appUser = _userManager.Users.SingleOrDefault(item => item.Id == accountId);
                if (appUser == null) return LocalRedirect(Url.GetLocalUrl());
            }
            else
            {
                if (!User.Identity.IsAuthenticated) return LocalRedirect(Url.GetLocalUrl());
                appUser = await _userManager.GetUserAsync(User);
            }

            vm.Id = appUser.Id;
            var userClaims = appUser.UserClaims.ToList();
            vm.NickName = userClaims.Get<string>(ClaimTypes.Name);
            vm.ComeFrom = userClaims.Get<string>(ClaimConsts.ComeFrom);
            vm.JoinDate = DateTime.Parse(userClaims.Get<string>(ClaimConsts.JoinDate));
            vm.Score = userClaims.Get(ClaimConsts.Score, int.Parse);
            vm.Signature = userClaims.Get<string>(ClaimConsts.Signature);

            var recentQuestions = appUser.Articles
                .Where(item => (item.State & ArticleState.Deleted) == 0)
                .OrderByDescending(item => item.PublishDate)
                .Take(10)
                .Select(item => new VmQuestionForHome(item));
            vm.RecentQuestions.AddRange(recentQuestions);

            var recentAnswers = appUser.ArticleComments
                .Where(item => (item.Article.State & ArticleState.Published) > 0)
                .OrderByDescending(item => item.ReplyDate)
                .Take(10)
                .Select(item => new VmAnswerForHome(item));
            vm.RecentAnswers.AddRange(recentAnswers);
            return View(vm);
        }


        #region 登录

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewBag.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]LoginForm input, [FromQuery]string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid) return View(input);

            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, true);
            if (result.Succeeded)
            {
                _logger.LogInformation($"{input.Email} 登录成功");
                return LocalRedirect(Url.GetLocalUrl(returnUrl));
            }

            ViewBag.Errors = result.IsLockedOut ? "用户已锁定" : "邮箱或者密码错误";
            return View(input);
        }

        [HttpGet]
        public IActionResult ExternalLogin(string returnUrl = null)
        {
            //var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            //var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            //return new ChallengeResult(provider, properties);
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword(string returnUrl = null)
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ResetPassword(string returnUrl = null)
        {
            return View();
        }

        #endregion

        #region 注册

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            HttpContext.Response.Cookies.Append("regid", Guid.NewGuid().ToString("N"));
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterForm input, [FromQuery]string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid) return View(input);

            var regId = Request.Cookies["regid"];
            var imgCodeKey = $"imgcode_{regId}";
            var imgCode = _memoryCache.Get<string>(imgCodeKey);
            if (imgCode == null || !imgCode.ToLower().Equals(input.ImageCode.ToLower()))
            {
                ModelState.AddModelError(nameof(input.ImageCode), "图像验证码无效");
                return View(input);
            }

            var verCodeKey = $"vercode_{regId}";
            var verCode = _memoryCache.Get<string>(verCodeKey);
            if (verCode == null || !verCode.ToLower().Equals(input.VerifyCode.ToLower()))
            {
                ModelState.AddModelError(nameof(input.VerifyCode), "验证码无效");
                return View(input);
            }

            var user = new AppUser { UserName = input.Email, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await _userManager.AddClaimsAsync(user, new List<Claim>
                {
                    new Claim(ClaimTypes.Name, input.NickName),
                    new Claim(ClaimConsts.JoinDate, DateTime.Now.ToString("yyyy-MM-dd"))
                });

                _logger.LogInformation($"{input.Email} 注册成功，等待激活");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, returnUrl, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(input.Email, callbackUrl);
                ViewBag.Success = "已向您填写的邮箱发送了账号激活邮件，请确认激活";
                return View();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(input);
        }

        public IActionResult GetImgCode()
        {
            var regId = Request.Cookies["regid"];
            var imgCode = CodeGenerator.CreateCode(4);
            var key = $"imgcode_{regId}";
            _memoryCache.Set(key, imgCode, TimeSpan.FromMinutes(5));
            return File(CodeGenerator.CreateImageBytes(imgCode), "image/jpeg", "imgcode.png");
        }

        [AcceptVerbs("GET")]
        public async Task<IActionResult> SendVerCode([FromQuery]string email, [FromQuery]string code)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code) || !Validator.CheckEmail(email))
                return Json(false);

            var regId = Request.Cookies["regid"];
            var key = $"imgcode_{regId}";
            var imgCode = _memoryCache.Get<string>(key);

            if (string.IsNullOrWhiteSpace(imgCode) || !code.ToLower().Equals(imgCode.ToLower()))
                return Json(false);

            var verCode = Guid.NewGuid().ToString("N").Substring(0, 6);
            var timeout = 5;
            await _emailSender.SendRegisterCodeAsync(email, verCode, timeout);

            key = $"vercode_{regId}";
            _memoryCache.Set(key, verCode, TimeSpan.FromMinutes(timeout));
            return Json(true);
        }

        [AcceptVerbs("GET")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            if (await _userManager.Users.AnyAsync(item => email.Equals(item.Email)))
                return Json($"{email} 已被注册");

            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code, string returnUrl = null)
        {
            if (userId == null || code == null)
                return Redirect("/Home/Index");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"加载ID为'{userId}'的用户失败.");
                ViewBag.Errors = "激活用户失败";
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                _logger.LogError($"激活ID为'{userId}'的用户失败.");
                ViewBag.Errors = "激活用户失败";
                return View();
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        #endregion

        #region 注销

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return LocalRedirect(Url.GetLocalUrl(returnUrl));
        }

        #endregion

        #region 主页

        public IActionResult GetAvatar([FromServices]IHostingEnvironment env, string accountId, int type = 1)
        {
            if (type < 0 || string.IsNullOrWhiteSpace(accountId)) return Json(false);

            var avatar = env.MapPath($"~/images/avatars/{accountId}.png");
            if (!SysFile.Exists(avatar))
            {
                avatar = env.MapPath("~/images/avatars/default.png");
            }
            return File(SysFile.OpenRead(avatar), "image/jpeg");
        }


        #endregion

        #region 个人中心

        [Authorize]
        public async Task<IActionResult> Index()
        {
            ViewBag.Selected = nameof(Index);

            var appUser = await _userManager.GetUserAsync(User);
            ViewBag.ArticleTotal = appUser.Articles.Count;
            ViewBag.FavoriteTotal = appUser.FavoriteArticles.Count;

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Article()
        {
            ViewBag.Selected = nameof(Article);

            var appUser = await _userManager.GetUserAsync(User);
            ViewBag.ArticleTotal = appUser.Articles.Count;
            ViewBag.FavoriteTotal = appUser.FavoriteArticles.Count;

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Message()
        {
            ViewBag.Selected = nameof(Message);
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMessage(NoticeReadState state, int start = 0, int length = 15)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var msgs = appUser.NoticeReceives
                .Where(item => item.Notice != null
                               && (item.Notice.State & NoticeState.Deleted) == 0
                               && (item.State & NoticeReadState.Deleted) == 0
                               && (item.State & state) > 0)
                .Select(item => new VmNoticeForAccount(item))
                .OrderBy(item => item.AtWhen)
                .ToList();

            return Json(msgs);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Setting()
        {
            ViewBag.Selected = nameof(Setting);

            var appUser = await _userManager.GetUserAsync(User);
            var userClaims = appUser.UserClaims;
            var vm = new VmAccountForBasic
            {
                Id = appUser.Id,
                NickName = userClaims.Get<string>(ClaimTypes.Name),
                Gender = userClaims.Get(ClaimTypes.Gender, int.Parse),
                ComeFrom = userClaims.Get<string>(ClaimConsts.ComeFrom),
                Signature = userClaims.Get<string>(ClaimConsts.Signature)
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Setting([FromForm]VmAccountForBasic vm)
        {
            ViewBag.Selected = nameof(Setting);

            if (!ModelState.IsValid) return BadRequest(vm);

            var appUser = await _userManager.GetUserAsync(User);

            var claims = new List<(string type, string value)>
            {
                (ClaimTypes.Name, vm.NickName),
                (ClaimTypes.Gender, vm.Gender.ToString())
            };

            if (!string.IsNullOrWhiteSpace(vm.ComeFrom))
                claims.Add((ClaimConsts.ComeFrom, vm.ComeFrom));

            if (!string.IsNullOrWhiteSpace(vm.Signature))
                claims.Add((ClaimConsts.Signature, vm.Signature));

            await _userManagerExt.ModifyClaims(appUser, claims);
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadAvatar([FromServices]IHostingEnvironment env, IFormFile file)
        {
            if (file == null || file.Length > 100 * 1024) return Json(false);

            var appUser = await _userManager.GetUserAsync(User);
            var path = env.MapPath($"~/images/avatars/{appUser.Id}.png");

            if (SysFile.Exists(path))
                SysFile.Delete(path);

            using (var stream = SysFile.Create(path))
            {
                await file.CopyToAsync(stream);
            }
            return Json(true);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ModifyPassword([FromForm]VmModifyPassword vm)
        {
            ViewBag.Selected = nameof(Setting);
            ViewBag.Msg = "修改失败";

            var appUser = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                var verifyResult = _userManager.PasswordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, vm.OldPassword);
                if (verifyResult == PasswordVerificationResult.Success)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    var identityResult = await _userManager.ResetPasswordAsync(appUser, token, vm.Password);
                    if (identityResult.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        await _signInManager.SignInAsync(appUser, false);
                        ViewBag.Msg = "修改成功";
                    }
                }
            }

            var userClaims = appUser.UserClaims;
            return View("Setting", new VmAccountForBasic
            {
                Id = appUser.Id,
                NickName = userClaims.Get<string>(ClaimTypes.Name),
                Gender = userClaims.Get(ClaimTypes.Gender, int.Parse),
                ComeFrom = userClaims.Get<string>(ClaimConsts.ComeFrom),
                Signature = userClaims.Get<string>(ClaimConsts.Signature)
            });
        }

        #endregion

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DailySignIn()
        {
            var appUser = await _userManager.GetUserAsync(User);
            return Json(await _userManagerExt.DailySignIn(appUser, HttpContext.GetClientUserIp()));
        }

        [HttpGet]
        public async Task<IActionResult> LoadDailySign()
        {
            var isTodaySignIn = false;
            var keepSignInDays = 0;
            var signInScore = DailySignRule.Default.Score;

            var appUser = await _userManager.GetUserAsync(User);
            if (appUser != null)
            {
                //今日是否签到
                isTodaySignIn = _userManagerExt.IsDailySignIn(appUser, DateTime.Now);
                //持续签到天数
                keepSignInDays = _userManagerExt.GetKeepSignInDays(appUser);
                //今日签到可获财富值
                signInScore = DailySignRule.MatchRule(keepSignInDays + (isTodaySignIn ? 0 : 1)).Score;
            }

            return Json(new
            {
                isTodaySignIn,
                keepSignInDays,
                signInScore
            });
        }

        /// <summary>
        /// 签到活跃榜 前15
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ActiveTop()
        {
            var vm = new VmActiveTop();
            var newestSignIns = await _userManagerExt.GetDailySignIns(where: item => item.SignInTime.Date == DateTime.Now.Date, orderBy: item => item.SignInTime);
            vm.Newest = newestSignIns.Select(item => new VmDailySignIn(item)).ToList();

            var fastestSignIns = await _userManagerExt.GetDailySignIns(where: item => item.SignInTime.Date == DateTime.Now.Date, isDesc: true, orderBy: item => item.SignInTime);
            vm.Fastest = fastestSignIns.Select(item => new VmDailySignIn(item)).ToList();

            var longestSignIns = await _userManagerExt.GetLongestDailySignIn(15);
            vm.Longest = longestSignIns.Select(item => new VmDailySignIn(item)).ToList();
            return View(vm);
        }

        [Authorize]
        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddFavorite([FromForm]string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId)) return Json(false);

            var appUser = await _userManager.GetUserAsync(User);
            return Json(await _userManagerExt.AddFavorite(appUser, articleId));
        }
    }
}
