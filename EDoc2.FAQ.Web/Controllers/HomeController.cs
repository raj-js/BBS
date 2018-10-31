using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using EDoc2.FAQ.Web.Extensions;
using EDoc2.FAQ.Web.Models;
using EDoc2.FAQ.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace EDoc2.FAQ.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IArticleManager _articleManager;
        private readonly ISystemManager _systemManager;
        private readonly IUserManagerExt _userManagerExt;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public HomeController(UserManager<AppUser> userManager,
            IArticleManager articleManager,
            ISystemManager systemManager,
            IUserManagerExt userManagerExt,
            IMemoryCache memoryCache,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _articleManager = articleManager;
            _systemManager = systemManager;
            _userManagerExt = userManagerExt;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string product = "", string category = "", string tag = "", string state = "")
        {
            Expression<Func<Article, bool>> where = item => (item.State & ArticleState.Published) > 0 && !item.IsTop;
            if (!string.IsNullOrWhiteSpace(product))
                where = where.And(e => e.ArticleCategories.Any(c => c.Category.SubCategory == ArticleSubTypes.Product && c.CategoryId == product));

            if (!string.IsNullOrWhiteSpace(category))
                where = where.And(e => e.ArticleCategories.Any(c => c.Category.SubCategory == ArticleSubTypes.Category && c.CategoryId == category));

            if (!string.IsNullOrWhiteSpace(tag))
                where = where.And(e => e.ArticleCategories.Any(c => c.Category.SubCategory == ArticleSubTypes.Tag && c.CategoryId == tag));

            if (!string.IsNullOrWhiteSpace(state) && Enum.TryParse<ArticleState>(state, true, out var articleState))
                where = where.And(item => (item.State & articleState) > 0);

            var vm = new VmHomeIndex
            {
                Total = await _articleManager.CountArticles(@where),
                Category = category,
                State = state,
                Tag = tag,
                Nav = new VmNav
                {
                    Product = product,
                    Category = category,
                    State = state,
                    Tag = tag
                }
            };

            var topReplies = await _articleManager.GetTopReplies(DateTime.Now.AddDays(-7), DateTime.Now, 16);
            topReplies.ForEach(g =>
            {
                var appUserName = _userManagerExt.GetUserName(g.appUserId);
                vm.TopReplies.Add(new VmTopReplie(g.appUserId, appUserName, g.replies));
            });
            return View(vm);
        }

        public IActionResult Error(string message)
        {
            ViewData["error"] = message;
            return View();
        }

        /// <summary>
        /// 举报
        /// </summary>
        [Authorize]
        [AcceptVerbs("POST")]
        public async Task<IActionResult> AddReport([FromForm]VmReport vm)
        {
            if (!ModelState.IsValid) return Json(false);

            var appUser = await _userManager.GetUserAsync(User);
            var report = new Report
            {
                ReporterId = appUser.Id,
                TargetId = vm.TargetId,
                ReportTargetType = vm.ReportTargetType,
                SubType = vm.SubType,
                Description = vm.Description,
                ReportDate = DateTime.Now,
                Result = ReportResult.Untreated
            };
            await _systemManager.AddReport(report);
            return Json(true);
        }
    }
}
