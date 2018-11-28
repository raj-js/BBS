using EDoc2.FAQ.Api.Models.Accounts;
using EDoc2.FAQ.Core.Application.Accounts;
using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAppService _accountAppService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountAppService accountAppService,
            ILogger<AccountController> logger)
        {
            _accountAppService = accountAppService ?? throw new ArgumentNullException(nameof(accountAppService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("register")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IdentityResult))]
        public async Task<IActionResult> Register([FromForm]VmRegisterReq vm)
        {
            if (!ModelState.IsValid) return BadRequest();

            var identityResult = await _accountAppService.Register(new AccountDtos.Register
            {
                Email = vm.Email,
                Nickname = vm.Nickname,
                Password = vm.Password
            });

            return Ok(identityResult);
        }
    }
}
