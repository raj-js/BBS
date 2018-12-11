using EDoc2.FAQ.Core.Application.Applications;
using EDoc2.FAQ.Core.Application.DtoBase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Applications.Dtos.ApplicationDtos;

namespace EDoc2.FAQ.Api.Controllers
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationAppService _applicationAppService;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="applicationAppService"></param>
        public ApplicationController(IApplicationAppService applicationAppService)
        {
            _applicationAppService = applicationAppService ?? throw new ArgumentNullException(nameof(applicationAppService));
        }

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper<Profile>))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _applicationAppService.GetSettings());
        }

        /// <summary>
        /// 更新系统设置
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(RespWapper))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateSettingsReq req)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _applicationAppService.UpdateSettings(req);
            return Ok(response);
        }
    }
}