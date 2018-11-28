using AutoMapper;
using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace EDoc2.FAQ.Api.Models.Admins
{
    public class VmGetAccountsReq : IPagingReq, IReqBase<AccountDtos.SearchReq>
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(128)]
        public string Nickname { get; set; }

        /// <summary>
        /// 邮箱 允许模糊查询
        /// </summary>
        [MaxLength(128)]
        public string Email { get; set; }

        /// <summary>
        /// 是否被屏蔽
        /// </summary>
        public bool? IsMuted { get; set; }

        /// <summary>
        /// 是否为版主
        /// </summary>
        public bool? IsModerator { get; set; }

        public string OrderBy { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsAscending { get; set; }

        public AccountDtos.SearchReq ToDto()
        {
            return new AccountDtos.SearchReq
            {
                Nickname = Nickname,
                Email = Email,
                IsMuted = IsMuted,
                IsModerator = IsModerator,
                OrderBy = OrderBy,
                IsAscending = IsAscending,
                Skip = PageSize * (PageIndex - 1),
                Take = PageSize
            };
        }
    }
}
