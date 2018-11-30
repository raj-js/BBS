using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Accounts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Application.Accounts.Dtos
{
    public class AccountDtos
    {
        #region Request

        public class SearchReq : IPagingRequest
        {
            [MaxLength(50)]
            public string Nickname { get; set; }

            [MaxLength(128)]
            public string Email { get; set; }

            public bool? IsMuted { get; set; }

            public bool? IsModerator { get; set; }

            public int Skip { get; set; }

            public int Take { get; set; }

            [MaxLength(50)]
            public string OrderBy { get; set; } = "Id";

            public bool IsAscending { get; set; }
        }

        public class GrantModeratorReq
        {
            [Required]
            public string UserId { get; set; }

            [Required]
            public Guid ModuleId { get; set; }
        }

        public class RegisterReq
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MaxLength(50)]
            public string Nickname { get; set; }

            [Required]
            [MaxLength(50)]
            public string Password { get; set; }
        }

        public class LoginReq
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MaxLength(50)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public class RetrievePasswordReq
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public class ResetPasswordReq
        {
            [Required]
            [MaxLength(50)]
            public string UserId { get; set; }

            [Required]
            public string Code { get; set; }

            [Required]
            [MaxLength(50)]
            public string Password { get; set; }
        }

        public class EditProfileReq: EntityDto<string>
        {
            
        }

        /// <summary>
        /// 关注用户请求
        /// </summary>
        public class FollowUserReq
        {
            /// <summary>
            /// 被关注的用户编号
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string TargetUserId { get; set; }
        }

        /// <summary>
        /// 取消关注用户请求
        /// </summary>
        public class UnFollowUserReq
        {
            /// <summary>
            /// 被取消关注的用户编号
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string TargetUserId { get; set; }
        }

        public class EmailConfirmReq
        {
            /// <summary>
            /// 用户编号
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string UserId { get; set; }

            /// <summary>
            /// 邮件确认校验 Token
            /// </summary>
            [Required]
            public string Code { get; set; }
        }

        #endregion

        #region Response

        public class ListItem : EntityDto<string>
        {
            /// <summary>
            /// 昵称
            /// </summary>
            public string Nickname { get; set; }

            /// <summary>
            /// 角色名称
            /// </summary>
            public string RoleName { get; set; }

            /// <summary>
            /// 邮件
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// 邮箱已确认
            /// </summary>
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// 加入日期
            /// </summary>
            public DateTime JoinDate { get; set; }

            public static ListItem From(User user)
            {
                return new ListItem
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    RoleName = string.Join(',', user.UserRoles.Select(s => s.Role?.Name).ToArray()),
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    JoinDate = user.JoinDate
                };
            }
        }

        public class Details: EntityDto<string>
        {
            public static Details From(User user)
            {
                return new Details();
            }
        }

        public class Profile: EntityDto<string>
        {
            public static Profile From(User user)
            {
                return new Profile();
            }
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        public class RetrievePasswordResp
        {
            /// <summary>
            /// 用户编号
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// 用户修改密码 Token
            /// </summary>
            public string Code { get; set; }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        public class RegisterResp
        {
            /// <summary>
            /// 是否注册成功
            /// </summary>
            public bool Succeeded { get; private set; }

            /// <summary>
            /// 用户编号
            /// </summary>
            public string UserId { get; private set; }

            /// <summary>
            /// 邮件确认 Token
            /// </summary>
            public string Code { get; private set; }

            /// <summary>
            /// 注册失败的错误信息
            /// </summary>
            public IdentityError[] Errors { get; private set; }

            public static RegisterResp Success(string userId, string code)
            {
                return new RegisterResp
                {
                    Succeeded = true,
                    UserId = userId,
                    Code = code
                };
            }

            public static RegisterResp Failed(params IdentityError[] errors)
            {
                return new RegisterResp
                {
                    Succeeded = false,
                    Errors = errors
                };
            }
        }

        #endregion
    }
}
