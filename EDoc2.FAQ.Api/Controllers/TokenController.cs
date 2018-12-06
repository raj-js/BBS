using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// Token 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="accountAppService"></param>
        public TokenController(IOptions<JwtSetting> jwtOptions,
            IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        /// <summary>
        /// 身份校验获取Token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<string>))]
        public async Task<IActionResult> Token([FromBody]LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.Authorize(req);
            return Ok(response);
        }
    }
}