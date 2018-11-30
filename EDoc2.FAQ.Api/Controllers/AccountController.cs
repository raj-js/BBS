using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 会员操作接口
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="accountAppService"></param>
        /// <param name="logger"></param>
        public AccountController(IAccountAppService accountAppService,
            ILogger<AccountController> logger)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IdentityResult))]
        public async Task<IActionResult> Register([FromForm]AccountDtos.RegisterReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var identityResult = await _accountAppService.Register(req);
            return Ok(identityResult);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SignInResult))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromForm]AccountDtos.LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var signInResult = await _accountAppService.Login(req);
            return Ok(signInResult);
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AccountDtos.Profile))]
        public async Task<IActionResult> GetProfile()
        {
            return Ok(await _accountAppService.GetUserProfile());
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("retrievePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AccountDtos.RetrievePasswordResp))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RetrievePassword([FromForm]AccountDtos.RetrievePasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var resetToken = await _accountAppService.GenerateResetPasswordToken(req);
            return Ok(resetToken);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResetPassword([FromForm]AccountDtos.ResetPasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _accountAppService.ResetPassword(req);
            return Ok();
        }
    }
}
