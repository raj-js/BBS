using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Applications
{
    public interface IApplicationRepository: IRepository<Application>
    {
        /// <summary>
        /// 获取默认实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Application> FindById(Guid id);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        Task Create(Application application);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="application"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task Update(Application application, params string[] properties);

        /// <summary>
        /// 增加设置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        Task AddSetting(ApplicationSetting setting);

        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task UpdateSetting(ApplicationSetting setting, params string[] properties);
    }
}
