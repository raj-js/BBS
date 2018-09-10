using Microsoft.AspNetCore.Mvc;

namespace EDoc2.FAQ.Web.Controllers
{
    public class ArticleController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Detail(long articleId)
        {
            return View();
        }
    }
}
