using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Threading.Tasks;
using EDoc2.FAQ.ImageCode;
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
using Microsoft.Extensions.Logging;

namespace EDoc2.FAQ.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserManagerExt _userManagerExt;
        private readonly IArticleManager _articleManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IUserManagerExt userManagerExt,
            IArticleManager articleManager,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ILogger<ArticleController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userManagerExt = userManagerExt;
            _articleManager = articleManager;
            _memoryCache = memoryCache;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Add()
        {
            var vm = new VmArticleForAdd();
            vm.PageId = Guid.NewGuid().ToString("N");

            var publisher = await _userManager.GetUserAsync(User);
            //判断财富值是否充足
            var publisherScore = publisher.UserClaims.Get(ClaimConsts.Score, int.Parse);
            vm.LeftScore = publisherScore;
            return View(vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public async Task<IActionResult> Add([FromForm]VmArticleForAdd vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var imgCodeKey = $"imgcode_{vm.PageId}";
            var imgCode = _memoryCache.Get<string>(imgCodeKey);
            if (imgCode == null || !imgCode.ToLower().Equals(vm.ImageCode.ToLower()))
            {
                ModelState.AddModelError(nameof(vm.ImageCode), "图像验证码无效");
                return View(vm);
            }

            var publisher = await _userManager.GetUserAsync(User);
            //判断财富值是否充足
            var publisherScore = publisher.UserClaims.Get(ClaimConsts.Score, int.Parse);
            if (publisherScore < vm.RewardScore)
            {
                ModelState.AddModelError(nameof(vm.RewardScore), "财富值不足");
                return View(vm);
            }

            var article = new Article
            {
                Id = Guid.NewGuid().ToString(),
                PublisherId = _userManager.GetUserId(User),
                Title = vm.Title,
                State = ArticleState.Published | ArticleState.NotSolve,
                PublishDate = DateTime.Now,
                IsTop = false,
                TopDate = null,
                IsTopTimeout = false,
                IsCream = false,
                CreamDate = null,
                IsCreamTimeout = false,
                Views = 0,
                Replies = 0,

                UseStorage = false,
                Content = vm.Content,
                RewardScore = vm.RewardScore,
                AdoptCommentId = string.Empty
            };

            var isApprove = _configuration.GetSection("Article").GetValue<bool>("IsApprove");
            if (isApprove)
            {
                article.State = ArticleState.Auditing;
                article.PublishDate = null;
            }

            var categories = new List<ArticleCategory>
            {
                new ArticleCategory
                {
                    ArticleId = article.Id,
                    CategoryId = vm.ProductId
                },
                new ArticleCategory
                {
                    ArticleId = article.Id,
                    CategoryId = vm.CategoryId
                },
                new ArticleCategory
                {
                    ArticleId = article.Id,
                    CategoryId = vm.TagId
                }
            };

            var entity = await _articleManager.AddArticle(publisher, article, categories);
            if (isApprove)
                return Redirect("/Article/Approve?articleId=" + entity.Id);

            //跳转至详情页（如果不需要审批）
            return Redirect("/Article/Detail?articleId=" + entity.Id);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetImgCode(string r, string _ = null)
        {
            if (string.IsNullOrWhiteSpace(r)) return File(new byte[0], "image/png");

            var imgCode = CodeGenerator.CreateCode(4);
            var key = $"imgcode_{r}";
            _memoryCache.Set(key, imgCode, TimeSpan.FromMinutes(5));
            return File(CodeGenerator.CreateImageBytes(imgCode), "image/jpeg", "imgcode.png");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId)) return Json(false);

            var article = await _articleManager.GetArticle(articleId);
            if (article == null) return Json(false);

            AppUser appUser = null;
            var vm = new VmArticleForDetail(article);
            if (User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.GetUserAsync(User);
                vm.IsFavorite = appUser.FavoriteArticles.Any(e => articleId.Equals(e.ArticleId) && e.State == FavoriteState.Favorite);
                vm.IsAuthor = vm.PublisherId.Equals(appUser.Id);
            }
            await _articleManager.ViewArticle(article, HttpContext.GetClientUserIp(), appUser);
            return View(vm);
        }

        [AcceptVerbs("Get")]
        public async Task<IActionResult> GetReplies(string articleId, int start = 0, int lenght = int.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(articleId)) return Json(false);

            var article = await _articleManager.GetArticle(articleId);
            if (article == null) return Json(false);

            var comments = _articleManager.GetArticleComments(article);
            var vmComments = comments.Select(item => new VmArticleCommentForDetail(item)).ToList();
            return Json(vmComments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitComment([FromForm]VmArticleCommentForAdd vm)
        {
            var returnUrl = "/Article/Detail?articleId=" + vm.ArticleId;

            if (!ModelState.IsValid) return Redirect(returnUrl);

            var article = await _articleManager.GetArticle(vm.ArticleId);
            if (article == null) return Redirect(returnUrl);

            var articleComment = new ArticleComment
            {
                ArticleId = article.Id,
                Content = vm.Content,
                IsReplyToComment = !string.IsNullOrWhiteSpace(vm.ToCommentId),
                ReplyCommentId = vm.ToCommentId,
                Goods = 0,
                Bads = 0,
                FromUserId = _userManager.GetUserId(User),
                ReplyDate = DateTime.Now
            };

            await _articleManager.AddArticleComment(article, articleComment);
            return Redirect(returnUrl);
        }

        [AcceptVerbs("Get")]
        public async Task<IActionResult> GetArticles(string product = null, string category = null, string tag = null, string state = null, int start = 0, int length = 20)
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

            var articles = await _articleManager.GetArticles(where, start, length, isDesc: false, orderby: item => item.PublishDate);
            return Json(articles.Select(item => new VmArticleForList(item)));
        }

        [AcceptVerbs("Get")]
        public async Task<IActionResult> GetBuzzTopics()
        {
            Expression<Func<Article, bool>> where = item => (item.State & ArticleState.Published) > 0
                                                            && item.PublishDate > DateTime.Now.AddDays(-7);
            var articles = await _articleManager.GetArticles(where, 0, 15, isDesc: false, orderby: item => item.Replies);
            return Json(articles.Select(item => new VmArticleForBuzz(item)));
        }

        [AcceptVerbs("Get")]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _articleManager.GetTags();
            return Json(tags.Select(item => new VmTag(item)));
        }

        [AcceptVerbs("Get")]
        public async Task<IActionResult> GetTopArticles(string product = null, string category = null, string tag = null, string state = null, int start = 0, int length = int.MaxValue)
        {
            Expression<Func<Article, bool>> where = item => (item.State & ArticleState.Published) > 0 && item.IsTop;
            if (!string.IsNullOrWhiteSpace(product))
                where = where.And(e => e.ArticleCategories.Any(c => c.Category.SubCategory == ArticleSubTypes.Product && c.CategoryId == product));

            if (!string.IsNullOrWhiteSpace(category))
                where = where.And(e => e.ArticleCategories.Any(c => c.Category.SubCategory == ArticleSubTypes.Category && c.CategoryId == category));

            if (!string.IsNullOrWhiteSpace(tag))
                where = where.And(e => e.ArticleCategories.Any(c => c.Category.SubCategory == ArticleSubTypes.Tag && c.CategoryId == tag));

            if (!string.IsNullOrWhiteSpace(state) && Enum.TryParse<ArticleState>(state, true, out var articleState))
                where = where.And(item => (item.State & articleState) > 0);

            var articles = await _articleManager.GetArticles(where, start, length, isDesc: false, orderby: item => item.TopDate);
            return Json(articles.Select(item => new VmArticleForList(item)));
        }

        [AcceptVerbs("Get")]
        [Authorize]
        public async Task<IActionResult> GetSelfArticles(int start = 0, int length = 15)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var articles = appUser.Articles.OrderByDescending(item => item.PublishDate)
                .Skip(start)
                .Take(length)
                .ToList();

            return Json(articles.Select(item => new VmArticleForAccountIndex(item)));
        }

        [AcceptVerbs("Get")]
        [Authorize]
        public async Task<IActionResult> GetSelfFavorite(int start = 0, int length = 15)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var articles = appUser.FavoriteArticles
                .Select(item => item.Article)
                .Where(item => (item.State & ArticleState.Published) > 0)
                .OrderByDescending(item => item.PublishDate)
                .Skip(start)
                .Take(length)
                .ToList();

            return Json(articles.Select(item => new VmArticleForAccountIndex(item)));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            ViewBag.Total = 0;
            ViewBag.Keyword = keyword;
            if (string.IsNullOrWhiteSpace(keyword)) return View();

            Expression<Func<Article, bool>> where = item => (item.State & ArticleState.Published) > 0 && item.Title.Contains(keyword);
            ViewBag.Total = await _articleManager.CountArticles(where);
            return View();
        }

        [AcceptVerbs("Get")]
        public async Task<IActionResult> SearchArticles(string keyword, int start = 0, int length = 15)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return Json(false);

            Expression<Func<Article, bool>> where = item => (item.State & ArticleState.Published) > 0 && item.Title.Contains(keyword);
            var articles = await _articleManager.GetArticles(where, start, length, isDesc: false, orderby: item => item.PublishDate);
            return Json(articles.Select(item => new VmArticleForList(item)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OperateComment(string type, string commentId)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(commentId)) return Json(false);

            bool isPraise;
            switch (type)
            {
                case "praise":
                    {
                        isPraise = true;
                        break;
                    }
                case "tread":
                    {
                        isPraise = false;
                        break;
                    }
                default:
                    {
                        return Json(false);
                    }
            }

            var appUser = await _userManager.GetUserAsync(User);
            return Json(await _articleManager.OperateComment(appUser, commentId, isPraise));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ClosingArticle([FromForm]VmCloseArticle vm)
        {
            if (string.IsNullOrWhiteSpace(vm.ArticleId)) return Json(false);
            if (vm.IsSatisfied && string.IsNullOrWhiteSpace(vm.CommentId)) return Json(false);

            var article = await _articleManager.GetArticle(vm.ArticleId);
            if (article == null) return Json(false);

            try
            {
                var appUser = await _userManager.GetUserAsync(User);
                //结贴
                if (!await _articleManager.CloseArticle(appUser, article, vm.IsSatisfied, vm.CommentId)) return Json(false);
                //散分
                var score = 0;
                AppUser awardWinner;
                var type = string.Empty;
                var description = string.Empty;

                if (vm.IsSatisfied)
                {
                    score = article.RewardScore;
                    //var awardWinnerId = article.Comments
                    //    .First(e => e.Id.Equals(vm.CommentId))
                    //    .FromUserId;

                    awardWinner = article.Comments.First(e => e.Id.Equals(vm.CommentId)).FromUser;
                    type = "满意结贴";
                    description = $"满意结帖，获得悬赏值";
                }
                else
                {
                    score = article.RewardScore / 2;
                    awardWinner = article.Publisher;

                    type = "无满意结贴";
                    description = $"无满意结贴，返还一半悬赏值";
                }
                return Json(await _userManagerExt.AddUserScore(awardWinner, type, description, score));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"关闭帖子异常, 帖子编号：{vm.ArticleId}, 回复编号：{vm.CommentId}, 满意结帖？{vm.IsSatisfied}");
                return Json(false);
            }
        }
    }
}
