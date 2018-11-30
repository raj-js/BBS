using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.ServiceBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Roles = "ADMINISTRATOR, MODERATOR")]
    public class AdminController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAccountAppService accountAppService, 
            ILogger<AdminController> logger)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("accounts")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagingDto<ListItem>))]
        public async Task<IActionResult> SearchAccounts([FromQuery]SearchReq req)
        {
            if (!ModelState.IsValid) return NotFound();

            return Ok(await _accountAppService.Search(req));
        }

        [HttpPut("grantModerator")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GrantModertor([FromForm]GrantModeratorReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            await Task.CompletedTask;

            return Ok();
        }


    }
}