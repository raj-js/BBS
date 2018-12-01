using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

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
        /// 当前是否登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("isAuthenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]AccountDtos.RegisterReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.Register(req);
            return Ok(response);
        }

        /// <summary>
        /// 邮箱确认
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("emailConfirm")]
        public async Task<IActionResult> EmailConfrim([FromBody]AccountDtos.EmailConfirmReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.EmailConfirm(req);
            return Ok(response);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]AccountDtos.LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.Login(req);
            return Ok(response);
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(string id = null)
        {
            var response = await _accountAppService.GetProfile(id);
            return Ok(response);
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("retrievePassword")]
        public async Task<IActionResult> RetrievePassword([FromBody]AccountDtos.RetrievePasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.GenerateResetPasswordToken(req);
            return Ok(response);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]AccountDtos.ResetPasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.ResetPassword(req);
            return Ok(response);
        }

        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody]AccountDtos.FollowUserReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.Follow(req.TargetUserId);
            return Ok(response);
        }

        /// <summary>
        /// 取消关注用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("unfollow")]
        public async Task<IActionResult> UnFollow([FromBody]AccountDtos.UnFollowUserReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.UnFollow(req.TargetUserId);
            return Ok(response);
        }

        /// <summary>
        /// 注销当前用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Logout()
        {
            var response = await _accountAppService.Logout();
            return Ok(response);
        }
    }
}
