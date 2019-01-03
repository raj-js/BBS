using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.DtoBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Infrastructure.Settings;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// Token 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="accountAppService"></param>
        public AuthorizeController(IOptions<JwtSetting> jwtOptions,
            IAccountAppService accountAppService)
        {
            _accountAppService = accountAppService;
        }

        /// <summary>
        /// 身份校验获取Token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("token")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<string>))]
        public async Task<IActionResult> Token([FromBody]LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.Authorize(req);
            if (!response.Success)
                return BadRequest(response.Errors);

            return Ok(response);
        }
    }
}