using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Applications;

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
            [MaxLength(128)]
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
            [MaxLength(128)]
            public string Email { get; set; }

            [Required]
            [MaxLength(50)]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }

        public class RetrievePasswordReq
        {
            [Required]
            [MaxLength(128)]
            public string Email { get; set; }
        }

        public class Edit: EntityDto<string>
        {
            
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

        #endregion
    }
}
