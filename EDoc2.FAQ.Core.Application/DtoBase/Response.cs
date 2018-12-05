using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Application.DtoBase
{
    public class Response
    {
        public bool Success { get; protected set; }

        public Error[] Errors { get; protected set; }

        public static Response Successed()
        {
            return new Response
            {
                Success = true
            };
        }

        public static Response Failed(params Error[] errors)
        {
            return new Response
            {
                Success = false,
                Errors = errors
            };
        }
    }

    public class Response<T> : Response
    {
        public T Body { get; private set; }

        public static Response<TBody> Successed<TBody>(TBody body = default(TBody))
        {
            return new Response<TBody>
            {
                Success = true,
                Body = body,
                Errors = null
            };
        }

        public static Response<TBody> Failed<TBody>(params Error[] errors) where TBody: class 
        {
            return new Response<TBody>
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

        public static Response ToResponse(this IdentityResult result)
        {
            return result.Succeeded ? Response.Successed() : Response.Failed(result.Errors.ToRespErrors());
        }

        public static Response ToResponse(this SignInResult result)
        {
            if (result.Succeeded)
                return Response.Successed();

            var errors = new List<Error>();

            if (result.IsLockedOut)
                errors.Add(new Error { Code = "LockOut", Description = "用户已锁定" });

            if (result.IsNotAllowed)
                errors.Add(new Error { Code = "NotAllowed", Description = "此用户不允许登录" });

            if (result.RequiresTwoFactor)
                errors.Add(new Error { Code = "RequiresTwoFactor", Description = "需要 TwoFactor 登录" });

            return Response.Failed(errors.ToArray());
        }
    }
}
