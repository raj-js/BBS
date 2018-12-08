using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Application.DtoBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 管理员操作
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="accountAppService"></param>
        /// <param name="logger"></param>
        public AdminController(IAccountAppService accountAppService, 
            ILogger<AdminController> logger)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("searchUsers")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<PagingDto<ListItem>>))]
        public async Task<IActionResult> SearchUsers([FromQuery]SearchReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            var response = await _accountAppService.Search(req);
            return Ok(response);
        }

        /// <summary>
        /// 屏蔽用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("muteUser")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        public async Task<IActionResult> MuteUser([FromQuery]string id)
        {
            if (id.IsNullOrEmpty()) return NotFound();

            var response = await _accountAppService.MuteUser(id);
            return Ok(response);
        }

        /// <summary>
        /// 撤销屏蔽用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("unmuteUser")]
        public async Task<IActionResult> UnMuteUser([FromQuery]string id)
        {
            if (id.IsNullOrEmpty()) return NotFound();

            var response = await _accountAppService.UnMuteUser(id);
            return Ok(response);
        }

        /// <summary>
        /// 授权为版主
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut("grantModerator")]
        public async Task<IActionResult> GrantModertor([FromBody]GrantModeratorReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            await Task.CompletedTask;

            return Ok();
        }
    }
}