using Microsoft.AspNetCore.Mvc;

namespace EDoc2.FAQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("/reg")]
        public ActionResult Register(string email, string nickname, string password)
        {
            return NotFound();
        }

    }
}
