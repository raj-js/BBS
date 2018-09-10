using System.Linq;
using EDoc2.FAQ.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDoc2.FAQ.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string nav = "", string subNav = "")
        {
            ViewData["Navs"] = Nav.SelectNav(nav);
            ViewData["SubNav"] = Nav.SelectSubNav(nav, subNav).ToList();
            return View();
        }

        public IActionResult Error(string message)
        {
            ViewData["error"] = message;
            return View();
        }
    }
}
