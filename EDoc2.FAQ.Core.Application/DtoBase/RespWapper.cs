using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Application.DtoBase
{
    public class RespWapper
    {
        public bool Success { get; protected set; }

        public Error[] Errors { get; protected set; }

        public static RespWapper Successed()
        {
            return new RespWapper
            {
                Success = true
            };
        }

        public static RespWapper Failed(params Error[] errors)
        {
            return new RespWapper
            {
                Success = false,
                Errors = errors
            };
        }
    }

    public class RespWapper<T> : RespWapper
    {
        public T Body { get; private set; }

        public static RespWapper<TBody> Successed<TBody>(TBody body = default(TBody))
        {
            return new RespWapper<TBody>
            {
                Success = true,
                Body = body,
                Errors = null
            };
        }

        public static RespWapper<TBody> Failed<TBody>(params Error[] errors) where TBody: class 
        {
            return new RespWapper<TBody>
            {
                Success = false,
                Errors = errors,
                Body = null
            };
        }
    }

    public class Error
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }

    public static class ErrorExtensions
    {
        public static Error ToRespError(this IdentityError error)
        {
            return new Error
            {
                Code = error.Code,
                Description = error.Description
            };
        }

        public static Error[] ToRespErrors(this IEnumerable<IdentityError> errors)
        {
            return errors.Select(e => e.ToRespError()).ToArray();
        }

        public static RespWapper ToResponse(this IdentityResult result)
        {
            return result.Succeeded ? RespWapper.Successed() : RespWapper.Failed(result.Errors.ToRespErrors());
        }

        public static RespWapper ToResponse(this SignInResult result)
        {
            if (result.Succeeded)
                return RespWapper.Successed();

            var errors = new List<Error>();

            if (result.IsLockedOut)
                errors.Add(new Error { Code = "LockOut", Description = "用户已锁定" });

            if (result.IsNotAllowed)
                errors.Add(new Error { Code = "NotAllowed", Description = "此用户不允许登录" });

            if (result.RequiresTwoFactor)
                errors.Add(new Error { Code = "RequiresTwoFactor", Description = "需要 TwoFactor 登录" });

            return RespWapper.Failed(errors.ToArray());
        }
    }
}
