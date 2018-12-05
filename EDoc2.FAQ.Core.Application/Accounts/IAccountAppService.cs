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
        Task<Response> Register(RegisterReq req);

        /// <summary>
        /// 邮箱确认
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> EmailConfirm(EmailConfirmReq req);

        /// <summary>
        /// 生成重置密码的Token
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> GenerateResetPasswordToken(RetrievePasswordReq req);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> ResetPassword(ResetPasswordReq req);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="req"></param>
        Task<Response> Login(LoginReq req);

        /// <summary>
        /// 注销当前用户
        /// </summary>
        /// <returns></returns>
        Task<Response> Logout();

        /// <summary>
        /// 分页搜索
        /// </summary>
        /// <param name="req"></param>
        /// <param name="skipAdmin">不检索管理员信息</param>
        /// <returns></returns>
        Task<Response<PagingDto<ListItem>>> Search(SearchReq req, bool skipAdmin = true);

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response> MuteUser(string userId);

        /// <summary>
        /// 撤销禁用用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response> UnMuteUser(string userId);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response> GetProfile(string userId = null);

        /// <summary>
        /// 更新个人资料
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<Response> EditProfile(EditProfileReq req);

        /// <summary>
        /// 关注用户
        /// </summary>
        /// <param name="userId"></param>
        Task<Response> Follow(string userId);

        /// <summary>
        /// 取消关注用户
        /// </summary>
        /// <param name="userId"></param>
        Task<Response> UnFollow(string userId);
    }
}
