using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 管理员操作
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<AdminController> _logger;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="accountAppService"></param>
        /// <param name="logger"></param>
        public AdminController(IAccountAppService accountAppService, 
            ILogger<AdminController> logger)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("searchUsers")]
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
        [HttpPut("muteUser")]
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