using EDoc2.FAQ.Api.Models.Accounts;
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
        public async Task<IActionResult> Register([FromForm]AccountDtos.RegisterReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var identityResult = await _accountAppService.Register(req);
            return Ok(identityResult);
        }

        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SignInResult))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromForm]AccountDtos.LoginReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var signInResult = await _accountAppService.Login(req);
            return Ok(signInResult);
        }

        [HttpPost("retrievePassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RetrievePassword([FromForm]AccountDtos.RetrievePasswordReq req)
        {
            if (!ModelState.IsValid) return BadRequest();


            //_accountAppService.GenerateResetPasswordToken(req)

            await Task.CompletedTask;
            return null;
        }
    }
}
