using EDoc2.FAQ.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Api.Infrastructure.Middlewares
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsHandlerMiddleware> _logger;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="next"></param>
        public ExceptionsHandlerMiddleware(RequestDelegate next,
            ILogger<ExceptionsHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 执行处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

                switch (e)
                {
                    case AccountNotFoundException _:
                    case EmailNotFoundException _:
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        }
                    case UnauthorizedAccessException _:
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            break;
                        }
                    case InvalidOperationException _:
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        }
                }
            }
        }
    }

    /// <summary>
    /// 异常处理拓展程序
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// 使用异常处理中间件
        /// </summary>
        /// <param name="builder"></param>
        public static void UseExceptionsHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionsHandlerMiddleware>();
        }
    }
}
