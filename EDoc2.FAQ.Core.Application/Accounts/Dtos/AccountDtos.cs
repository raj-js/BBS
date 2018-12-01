using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Domain.Accounts;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EDoc2.FAQ.Core.Application.Accounts.Dtos
{
    public class AccountDtos
    {
        #region Request

        /// <summary>
        /// 搜索用户
        /// </summary>
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

        /// <summary>
        /// 版主授权
        /// </summary>
        public class GrantModeratorReq
        {
            [Required]
            public string UserId { get; set; }

            [Required]
            public Guid ModuleId { get; set; }
        }

        /// <summary>
        /// 注册
        /// </summary>
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

        /// <summary>
        /// 登录
        /// </summary>
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

        /// <summary>
        /// 找回密码
        /// </summary>
        public class RetrievePasswordReq
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
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

        /// <summary>
        /// 修改个人
        /// </summary>
        public class EditProfileReq : EntityDto<string>
        {
            [Required]
            [MaxLength(50)]
            public string Nickname { get; set; }

            [MaxLength(128)]
            public string Signature { get; set; }

            [Required]
            public Gender Gender { get; set; }

            [MaxLength(128)]
            public string City { get; set; }
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

        /// <summary>
        /// 确认邮件
        /// </summary>
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

        /// <summary>
        /// 用户列表项
        /// </summary>
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

            /// <summary>
            /// 是否被屏蔽
            /// </summary>
            public bool IsMuted { get; set; }

            public static ListItem From(User user)
            {
                return new ListItem
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    RoleName = string.Join(',', user.UserRoles.Select(s => s.Role?.Name).ToArray()),
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    JoinDate = user.JoinDate,
                    IsMuted = user.IsMuted
                };
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public class Profile : EntityDto<string>
        {
            public string Nickname { get; set; }
            public string Signature { get; set; }
            public Gender Gender { get; set; }
            public string Email { get; set; }
            public string City { get; set; }
            public DateTime JoinDate { get; set; }
            public int Follows { get; set; }
            public int Fans { get; set; }
            public int Score { get; set; }
            public bool IsMuted { get; set; }

            public static Profile From(User user)
            {
                return new Profile
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    Signature = user.Signature,
                    Gender = user.Gender,
                    Email = user.Email,
                    City = user.City,
                    JoinDate = user.JoinDate,
                    Follows = user.Follows,
                    Fans = user.Fans,
                    Score = user.Score,
                    IsMuted = user.IsMuted
                };
            }
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        public class RetrievePassword
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
        public class Register
        {
            /// <summary>
            /// 用户编号
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// 邮件确认 Token
            /// </summary>
            public string Code { get; set; }
        }

        #endregion
    }
}
