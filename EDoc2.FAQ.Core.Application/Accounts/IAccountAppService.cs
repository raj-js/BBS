using System;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Core.Application.Accounts
{
    public interface IAccountAppService: IAppService
    {
        /// <summary>
        /// 邮件地址是否已经注册
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> IsEmailRegistered(string email);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="req"></param>
        Task<RespWapper> Register(RegisterReq req);

        /// <summary>
        /// 邮箱确认
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> EmailConfirm(EmailConfirmReq req);

        /// <summary>
        /// 生成重置密码的Token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> RetrievePassword(RetrievePasswordReq req);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> ResetPassword(ResetPasswordReq req);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="req"></param>
        Task<RespWapper> Authorize(LoginReq req);

        /// <summary>
        /// 注销当前用户
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> Logout();

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="req"></param>
        /// <param name="skipAdmin">不检索管理员信息</param>
        /// <returns></returns>
        Task<RespWapper<PagingDto<ListItem>>> Search(SearchReq req, bool skipAdmin = true);

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RespWapper> MuteUser(string userId);

        /// <summary>
        /// 撤销禁用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RespWapper> UnMuteUser(string userId);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RespWapper> GetProfile(string userId = null);

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> EditProfile(EditProfileReq req);

        /// <summary>
        /// 关注/取消关注用户
        /// </summary>
        /// <param name="userId"></param>
        Task<RespWapper> FollowOrNot(string userId);

        /// <summary>
        /// 获取用户的关注用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> GetFollows(GetFollowOrFansReq req);

        /// <summary>
        /// 获取用户的粉丝
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> GetFans(GetFollowOrFansReq req);

        /// <summary>
        /// 收藏或者取消
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> FavoriteOrNot(Guid id);

        /// <summary>
        /// 是否收藏
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> IsFavorite(Guid id);

        /// <summary>
        /// 是否关注某用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> IsFollow(string id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<RespWapper> ModifyPassword(ModifyPasswordReq req);

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="avatar"></param>
        /// <returns></returns>
        Task<RespWapper> ModifyAvatar(byte[] avatar);

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RespWapper> GetAvatar(string id);
    }
}
