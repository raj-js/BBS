using System;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using static EDoc2.FAQ.Core.Application.Notifies.Dtos.NotifyDtos;

namespace EDoc2.FAQ.Core.Application.Notifies
{
    public interface INotifyAppService: IAppService
    {
        /// <summary>
        /// 搜索通知（管理者使用）
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> Search(SearchReq req);

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> DeleteNotify(Guid id);

        /// <summary>
        /// 获取我的通知
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> SearchSelfNotifies(SearchSelfNotifyReq req);

        /// <summary>
        /// 阅读我的通知
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> ReadMyNotify(Guid id);

        /// <summary>
        /// 删除我的通知
        /// </summary>
        /// <returns></returns>
        Task<RespWapper> DeleteMyNotify(Guid id);
    }
}
