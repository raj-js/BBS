using System.Threading.Tasks;
using EDoc2.FAQ.Core.Application.Authorization.Dtos;
using EDoc2.FAQ.Core.Application.ServiceBase;
using Microsoft.AspNetCore.Identity;

namespace EDoc2.FAQ.Core.Application.Authorization
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
        /// 分页搜索
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagingDto<UserDtos.ListItem>> Search(UserDtos.Search dto);
    }
}
