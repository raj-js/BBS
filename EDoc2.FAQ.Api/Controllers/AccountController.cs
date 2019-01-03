using EDoc2.FAQ.Api.Infrastructure;
using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

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

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="accountAppService"></param>
        /// <param name="logger"></param>
        public AccountController(IAccountAppService accountAppService,
            ILogger<AccountController> logger)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
        }

        /// <summary>
        /// 当前是否登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("isSignIn")]
        [JwtAuthorize]
        public IActionResult IsSignIn()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> Register([FromBody]RegisterReq req)
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<RegisterResp>))]
        public async Task<IActionResult> EmailConfrim([FromBody]EmailConfirmReq req)
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> Login([FromBody]LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.Authorize(req);
            return Ok(response);
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<ProfileResp>))]
        public async Task<IActionResult> GetProfile(string id = null)
        {
            
            var response = await _accountAppService.GetProfile(id);
            return Ok(response);
        }

        /// <summary>
        /// 更新个人信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("editProfile")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<ProfileResp>))]
        [JwtAuthorize]
        public async Task<IActionResult> EditProfile(EditProfileReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.EditProfile(req);
            return Ok(response);
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("retrievePassword")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> RetrievePassword([FromBody]RetrievePasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.RetrievePassword(req);
            return Ok(response);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.ResetPassword(req);
            if (!response.Success)
                return BadRequest(response.Errors);

            return Ok(response);
        }

        /// <summary>
        /// 关注/取消关注用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("followOrNot")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        [JwtAuthorize]
        public async Task<IActionResult> FollowOrNot([FromBody]string id)
        {
            if (id.IsNullOrEmpty()) return BadRequest();

            var response = await _accountAppService.FollowOrNot(id);
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

        /// <summary>
        /// 获取关注
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("getFollows")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<UserSimpleResp>>))]
        public async Task<IActionResult> GetFollows([FromQuery]GetFollowOrFansReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.GetFollows(req);
            return Ok(response);
        }

        /// <summary>
        /// 获取粉丝
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("getFans")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<UserSimpleResp>>))]
        public async Task<IActionResult> GetFans([FromQuery]GetFollowOrFansReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.GetFans(req);
            return Ok(response);
        }

        /// <summary>
        /// 添加/移除收藏
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("favorite")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        [JwtAuthorize]
        public async Task<IActionResult> FavoriteOrNot([FromBody]Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.FavoriteOrNot(id);
            return Ok(response);
        }

        /// <summary>
        /// 判断是否收藏
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("isfavorite")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<bool>))]
        [JwtAuthorize]
        public async Task<IActionResult> IsFavorite([FromQuery]Guid id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.IsFavorite(id);
            return Ok(response);
        }

        /// <summary>
        /// 判断是否关注了某用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("isfollow")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<bool>))]
        [JwtAuthorize]
        public async Task<IActionResult> IsFollow([FromQuery]string id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.IsFollow(id);
            return Ok(response);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("modifyPassword")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        [JwtAuthorize]
        public async Task<IActionResult> ModifyPassword([FromBody]ModifyPasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _accountAppService.ModifyPassword(req);
            return Ok(response);
        }

        /// <summary>
        /// 修改用户头像
        /// </summary>
        /// <returns></returns>
        [HttpPost("modifyAvatar")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<string>))]
        [JwtAuthorize]
        public async Task<IActionResult> ModifyAvatar(IFormFile file)
        {
            if (!ModelState.IsValid) return BadRequest();

            var buffer = new byte[file.Length];
            await file.OpenReadStream()
                .ReadAsync(buffer, 0, buffer.Length);

            var response = await _accountAppService.ModifyAvatar(buffer);
            return Ok(response);
        }

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("avatar/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAvatar(string id)
        {
            if (id.IsNullOrEmpty()) return NotFound();

            var response = await _accountAppService.GetAvatar(id);

            if (!response.Success) return NotFound();

            return File((response as RespWapper<byte[]>)?.Body, "image/jpg");
        }

    }
}
