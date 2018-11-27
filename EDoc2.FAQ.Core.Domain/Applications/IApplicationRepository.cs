using EDoc2.FAQ.Core.Domain.SeedWork;
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
        Application GetDefault(Guid id);

        /// <summary>
        /// 获取默认实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Application> GetDefaultAsync(Guid id);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        Application Initialize(Application application);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        Task<Application> UpdateAsync(Application application);
    }
}
