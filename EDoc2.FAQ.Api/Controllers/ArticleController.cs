using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 文章管理
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            return null;
        }
    }
}