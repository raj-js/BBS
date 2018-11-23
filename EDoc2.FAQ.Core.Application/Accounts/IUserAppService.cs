using System;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using EDoc2.FAQ.Core.Application.ServiceBase;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Application.Accounts
{
    public interface IUserAppService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="dto"></param>
        Task<SignInResult> Login(UserDtos.Login dto);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="dto"></param>
        Task<IdentityResult> Register(UserDtos.Register dto);

        /// <summary>
        /// 生成重置密码的Token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<string> GenerateResetPasswordToken(string email);

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingDto<UserDtos.ListItem>> Search(UserDtos.Search dto);

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId"></param>
        void MuteUser(string userId);

        /// <summary>
        /// 将用户设置为板块版主
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="moduleId"></param>
        void GrantModerator(string userId, Guid moduleId);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDtos.Details> GetUserDetails(string userId);

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="editDto"></param>
        /// <returns></returns>
        UserDtos.Details EditProfile(UserDtos.Edit editDto);

        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="userId"></param>
        void Follow(string userId);

        /// <summary>
        /// 取消关注用户
        /// </summary>
        /// <param name="userId"></param>
        void UnFollow(string userId);


    }
}
