using System;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Authorization;

namespace EDoc2.FAQ.Core.Application.Authorization.Dtos
{
    public class UserDtos
    {
        #region Request

        public class Search : IPagingRequest
        {
            public string Nickname { get; set; }

            public string Email { get; set; }

            public int Skip { get; set; }

            public int Take { get; set; }

            public string OrderBy { get; set; }

            public bool IsAsc { get; set; }
        }

        public class Register
        {
            public string Email { get; set; }

            public string Nickname { get; set; }

            public string Password { get; set; }
        }

        public class Login
        {
            public string Email { get; set; }

            public string Password { get; set; }

            public bool RememberMe { get; set; }
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
            /// 邮件
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// 邮箱已确认
            /// </summary>
            public bool EmailConfirmed { get; set; }

            /// <summary>
            /// 手机号码
            /// </summary>
            public string PhoneNumber { get; set; }

            /// <summary>
            /// 加入日期
            /// </summary>
            public DateTime JoinDate { get; set; }

            public static ListItem From(User user)
            {
                return new ListItem
                {
                    Nickname = user.Nickname,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    JoinDate = user.JoinDate
                };
            }
        }

        #endregion
    }
}
