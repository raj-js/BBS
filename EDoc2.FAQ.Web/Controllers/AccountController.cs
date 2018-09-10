using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Web.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public IActionResult Login()
        {
            return Redirect("~/Home/Index");
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 查看其他人信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [Route("{accountId}")]
        [HttpGet]
        public IActionResult Index(long accountId)
        {
            return View();
        }

        /// <summary>
        /// 查看个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Home()
        {
            return View();
        }
    }
}